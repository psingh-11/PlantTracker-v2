using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using AutoMapper;
using PlantDAL.EDMX;
using PlantTracker.Models.Dto;

namespace PlantTracker.Mappers
{
    public static class PlantMapper
    {         
        public static Plant MapDtoToDAL(PlantDto plantDto, List<Images> imgList)
        {
            Plant plant = new Plant();
            plant.UserID = plantDto.UserID;
            plant.ID = plantDto.ID;
            plant.Name = plantDto.Name;
            plant.Type = plantDto.Type;
            plant.Genus = plantDto.Genus;
            plant.Species = plantDto.Species;
            plant.SubSpecies = plantDto.SubSpecies;
            plant.Count = plantDto.Count;
            plant.Notes = plantDto.Notes;
            plant.Images = imgList;
            if(plantDto.ParentOneID != null)
                plant.ParentOneID = Guid.Parse(plantDto.ParentOneID);
            if(plantDto.ParentTwoID != null)
                plant.ParentTwoID = Guid.Parse(plantDto.ParentTwoID);

            return plant;
        }

        public static PlantDto MapDALToDto(Plant plant)
        {
            PlantDto dto = new PlantDto();
            dto.UserID = plant.UserID;
            dto.ID = plant.ID;
            dto.Name = plant.Name;
            dto.Type = plant.Type;
            dto.Genus = plant.Genus;
            dto.Species = plant.Species;
            dto.SubSpecies = plant.SubSpecies;
            dto.Count = plant.Count;
            dto.Notes = plant.Notes;
            dto.ParentOneID = plant.ParentOneID.ToString();
            dto.ParentTwoID = plant.ParentTwoID.ToString();

            return dto;
        }
    }
}