using EventPlusWebAPI.BdContextEvent;
using MailKit.Net.Smtp;
using MailKit.Security;
using Microsoft.EntityFrameworkCore;
using MimeKit;

namespace EventPlusWebAPI.Services
{
    public class EmailService
    {
        private readonly EventContext _context;
        private readonly IConfiguration _configuration;

        public EmailService(
            EventContext context,
            IConfiguration configuration)
        {
            _context = context;
            _configuration = configuration;
        }

        // Envia o e-mail
        private async Task EnviarEmailAsync(
            string emailUsuario,
            string nomeEvento,
            DateTime dataEvento,
            string descricao)
        {
            var mensagem = new MimeMessage();

            mensagem.From.Add(new MailboxAddress(
                "Event+",
                _configuration["Email:Remetente"]
            ));

            mensagem.To.Add(
                MailboxAddress.Parse(emailUsuario)
            );

            mensagem.Subject = $"Lembrete: {nomeEvento}";

            mensagem.Body = new TextPart("html")
            {
                Text = $"""
                    <h1>Olá!</h1>

                    <p>
                        O evento <strong>{nomeEvento}</strong>
                        acontecerá em menos de 24 horas.
                    </p>

                    <p>
                        <strong>Data:</strong>
                        {dataEvento:dd/MM/yyyy HH:mm}
                    </p>

                    <p>
                        <strong>Descrição:</strong>
                        {descricao}
                    </p>

                    <p>
                        Não se esqueça de participar!
                    </p>
                    """
            };

            using var smtp = new SmtpClient();

            await smtp.ConnectAsync(
                _configuration["Email:Smtp"],
                int.Parse(_configuration["Email:Porta"]),
                SecureSocketOptions.StartTls
            );

            await smtp.AuthenticateAsync(
                _configuration["Email:Remetente"],
                _configuration["Email:Senha"]
            );

            await smtp.SendAsync(mensagem);

            await smtp.DisconnectAsync(true);
        }

        // Busca um evento específico
        public async Task BuscarEvento(Guid idEvento)
        {
            var evento = await _context.Evento
                .FirstOrDefaultAsync(e => e.IdEvento == idEvento);

            if (evento == null)
                return;

            var nome = evento.NomeEvento;
            var data = evento.DataEvento;
            var descricao = evento.Descricao;
        }

        // Verifica eventos que acontecerão nas próximas 24 horas
        public async Task VerificarEventos()
        {
            var agora = DateTime.Now;

            var eventos = await _context.Evento
                .ToListAsync();

            foreach (var evento in eventos)
            {
                var tempoRestante = evento.DataEvento - agora;

                Console.WriteLine("--------------------------------");
                Console.WriteLine($"Evento: {evento.NomeEvento}");
                Console.WriteLine($"Data: {evento.DataEvento}");
                Console.WriteLine($"Agora: {agora}");
                Console.WriteLine(
                    $"Faltam: {tempoRestante.TotalHours:F2} horas"
                );

                if (tempoRestante.TotalHours <= 24 &&
                    tempoRestante.TotalHours > 0)
                {
                    Console.WriteLine("Evento está dentro das 24 horas!");
                    Console.WriteLine("Enviando e-mail...");

                    await EnviarEmailAsync(
                        "samuelrenatobackup@gmail.com",
                        evento.NomeEvento,
                        evento.DataEvento,
                        evento.Descricao
                    );

                    Console.WriteLine("E-mail enviado!");
                }
                else
                {
                    Console.WriteLine(
                        "Evento não está dentro das próximas 24 horas."
                    );
                }
            }
        }
    }
}