using Microsoft.Azure.Storage;
using Microsoft.Azure.Storage.Blob;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Text;
using System.Threading.Tasks;

namespace CookTime
{
    class ImageUploader
    {
        private static ImageUploader instance_photos = null;
        private static ImageUploader instance_logos = null;
        private static ImageUploader instance_recipes = null;
        private string cadena_acceso;
        private CloudStorageAccount cuenta_azure;
        private CloudBlobClient cliente_blob;
        private CloudBlobContainer cliente_contenedor;

        /// <summary>
        /// Clase que permite subir imágenes a un contenedor de Azure Storage.
        /// </summary>
        /// <param name="contenedor">El nombre del contenedor que contendrá la imagen. Puede ser photos, logos o recipes.</param>
        private ImageUploader(string contenedor)
        {
            this.cadena_acceso =  "DefaultEndpointsProtocol=https;AccountName=cooktimephotos;AccountKey=F9WGhZLi44bbedXfIDYoPpk0wBqfFl25Mrot1Wk6y3AhhyRt6VRrrxKfj+KxY0eTua9Hb3WnbhyPJ6UeaRwYXg==;EndpointSuffix=core.windows.net";
            this.cuenta_azure = CloudStorageAccount.Parse(cadena_acceso);
            this.cliente_blob = cuenta_azure.CreateCloudBlobClient();
            this.cliente_contenedor = cliente_blob.GetContainerReference(contenedor);
        }

        /// <summary>
        /// Crea una instancia única de ImageUploader para subir imágenes a photos.
        /// </summary>
        /// <returns>Una instancia única de ImageUploader para photos.</returns>
        public static ImageUploader get_instance_photos()
        {
            if(instance_photos == null)
            {
                instance_photos = new ImageUploader("photos");
            }
            return instance_photos;
        }

        /// <summary>
        /// Crea una instancia única de ImageUploader para subir imágenes a logos.
        /// </summary>
        /// <returns>Una instancia única de ImageUploader para logos.</returns>
        public static ImageUploader get_instance_logos()
        {
            if (instance_logos == null)
            {
                instance_logos = new ImageUploader("logos");
            }
            return instance_logos;
        }

        /// <summary>
        /// Crea una instancia única de ImageUploader para subir imágenes a recipes.
        /// </summary>
        /// <returns>Una instancia única de ImageUploader para recipes.</returns>
        public static ImageUploader get_instance_recipes()
        {
            if (instance_recipes == null)
            {
                instance_recipes = new ImageUploader("recipes");
            }
            return instance_recipes;
        }

        /// <summary>
        /// Sube una imagen al contenedor de Azure Storage.
        /// </summary>
        /// <param name="stream">El stream de la imagen a subir.</param>
        public async void subir_imagen(Stream stream, string contenedor)
        {
            string nombre_archivo = new Random().Next(10000).ToString();
            CloudBlockBlob blob = cliente_contenedor.GetBlockBlobReference(nombre_archivo);
            await blob.UploadFromStreamAsync(stream);
            if(contenedor == "photos")
            {
                Cliente.get_instance().cambiar_foto_usuario(blob.Uri);
            }
            else if(contenedor.Split('&')[0] == "logos")
            {
                Cliente.get_instance().cambiar_logo(blob.Uri, contenedor.Split('&')[1]);
            }
        }

        public async void subir_foto_receta(Stream stream, string contenedor, Receta receta)
        {
            string nombre_archivo = new Random().Next(10000).ToString();
            CloudBlockBlob blob = cliente_contenedor.GetBlockBlobReference(nombre_archivo);
            await blob.UploadFromStreamAsync(stream);
            receta.foto = blob.Uri.ToString();

            if (contenedor.Split('&')[0] == "recipes")
            {
                Cliente.get_instance().cambiar_foto_receta(blob.Uri, contenedor.Split('&')[1]);
            }
        }

    }
}
