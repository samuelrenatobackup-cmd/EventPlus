using CloudinaryDotNet;
using EventPlusWebAPI.Interfaces;
using EventPlusWebAPI.Models;
using Microsoft.Extensions.Options;
using EventPlusWebAPI.Utils;
using CloudinaryDotNet.Actions;

namespace EventPlusWebAPI.Services
{
    public class CloudinaryService : ICloudinaryService
    {
        private readonly Cloudinary _cloudinary;

        public CloudinaryService(IOptions<CloudinarySettings> options)
        {

            var credenciais = options.Value;
            var account = new Account(credenciais.CloudName, credenciais.ApiKey, credenciais.ApiSecret);

            _cloudinary = new Cloudinary(account);
            _cloudinary.Api.Secure = true;

        }
        public async Task<string> UploadImagem(IFormFile arquivo)
        {
            using var stream = arquivo.OpenReadStream();

            var uploadParams = new ImageUploadParams
            {
                File = new FileDescription(arquivo.FileName, stream),
                Folder = "eventplus/eventoss"
            };

            var resultado = await _cloudinary.UploadAsync(uploadParams);

            return resultado.SecureUrl.AbsoluteUri;
        }

    }
}