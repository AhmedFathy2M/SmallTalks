using Microsoft.AspNetCore.Http;

namespace Core.Interfaces
{
	public interface IAzureBlobService
	{
		Task<string> UploadToAzureBlob(string containerName, IFormFile file);
	}
}
