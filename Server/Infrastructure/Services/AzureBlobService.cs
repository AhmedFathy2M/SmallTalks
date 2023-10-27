using Azure.Storage.Blobs;
using Azure.Storage.Blobs.Models;
using Core.Interfaces;
using Infrastructure.Helpers;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;

namespace Infrastructure.Services
{

	public class AzureBlobService : IAzureBlobService
	{
		private readonly BlobServiceClient _blobClient;

		public AzureBlobService(IConfiguration config)
		{
			_blobClient = new BlobServiceClient(config["AzureBlob:ConnectionString"]);
		}

		public async Task<string> UploadToAzureBlob(string containerName, IFormFile file)
		{
			var containerClient = _blobClient.GetBlobContainerClient(containerName);

			var fileName = $"{Guid.NewGuid()}{Path.GetExtension(file.FileName)}";

			var blobClient = containerClient.GetBlobClient(fileName);

			var options = new BlobUploadOptions
			{
				HttpHeaders = new BlobHttpHeaders { ContentType = file.ContentType }
			};

			using var fileStream = file.OpenReadStream();
			using var uploadedStream = new MemoryStream();
			await fileStream.CopyToAsync(uploadedStream);


			var binaryContent = new BinaryData(uploadedStream.ToArray());
			await blobClient.UploadAsync(binaryContent, options);
			return $"{_blobClient.Uri}{containerName}/{fileName}";
		}
	}
}
