using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using PlantDAL.EDMX;
using PlantTracker.Models.Dto;

namespace PlantTracker.Mapper
{
    public class ImageMapper
    {
        public static void MapHTTPToImage(List<HttpPostedFileBase> httpImages, PlantDto plant, string serverPath)
        {
            var plantDir = serverPath;
            if (!Directory.Exists(plantDir))
            {
                Directory.CreateDirectory(plantDir);
            }

            foreach (HttpPostedFileBase file in plant.Images)
            {
                Guid imageId = Guid.NewGuid();
                if (file != null)
                {
                    string extension = Path.GetExtension(file.FileName);
                    var ServerSavePath = Path.Combine(plantDir, imageId + extension);

                    Images img = new Images();
                    img.ID = imageId;
                    img.ImageFilePath = ServerSavePath;
                    img.PlantID = plant.ID;

                    file.SaveAs(ServerSavePath);
                }
            }
        }
    }
}