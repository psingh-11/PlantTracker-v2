using PlantDAL.EDMX;
using PlantTracker.Models.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PlantTracker.Mappers
{
    public class CustomValueMapper
    {
        public static object MapDtoToDAL(CustomValueDto customValue, int i)
        {
            var cv = new CustomValues();

            if(i == 1)
            {   
                cv.ID = customValue.ID;
                cv.Name = customValue.Name;
                cv.Notes = customValue.Notes;
            }
            else if(i == 2)
            {
                cv.ID = customValue.ID;
                cv.Name = customValue.Name;
                cv.Notes = customValue.Notes;
            }
            else if(i == 3)
            {
                cv.ID = customValue.ID;
                cv.Name = customValue.Name;
                cv.Notes = customValue.Notes;
            }
            else if(i == 4)
            {
                cv.ID = customValue.ID;
                cv.Name = customValue.Name;
                cv.Notes = customValue.Notes;
            }
            else
            {
                cv.ID = customValue.ID;
                cv.Name = customValue.Name;
                cv.Notes = customValue.Notes;
            }


            return cv;
        }

        public static object MapDALToDto(CustomValues customValue, int i)
        {
            var cv = new CustomValueDto();

            if (i == 1)
            {

                cv.ID = customValue.ID;
                cv.Name = customValue.Name;
                cv.Notes = customValue.Notes;
            }
            else if (i == 2)
            {
                cv.ID = customValue.ID;
                cv.Name = customValue.Name;
                cv.Notes = customValue.Notes;
            }
            else if (i == 3)
            {
                cv.ID = customValue.ID;
                cv.Name = customValue.Name;
                cv.Notes = customValue.Notes;
            }
            else if (i == 4)
            {
                cv.ID = customValue.ID;
                cv.Name = customValue.Name;
                cv.Notes = customValue.Notes;
            }
            else
            {
                cv.ID = customValue.ID;
                cv.Name = customValue.Name;
                cv.Notes = customValue.Notes;
            }


            return cv;
        }

    }
}