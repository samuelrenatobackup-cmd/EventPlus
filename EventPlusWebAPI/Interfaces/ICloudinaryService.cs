using EventPlusWebAPI.Models;

namespace EventPlusWebAPI.Interfaces
{
    public interface ICloudinaryService
    {
        Task<string> UploadImagem(IFormFile arquivo);
    }
}