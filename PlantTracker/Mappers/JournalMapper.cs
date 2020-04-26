using PlantDAL.EDMX;
using PlantTracker.Models.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PlantTracker.Mappers
{
    public static class JournalMapper
    {
        public static Journal MapDtoToDAL(JournalDto journalDto, List<Images> imgList)
        {
            Journal journal = new Journal();
            journal.UserID = journalDto.UserID;
            journal.ID = journalDto.ID;
            journal.Name = journalDto.Name;
            journal.Notes = journalDto.Notes;
            journal.Images = imgList;
            if(journalDto.PlantId != null)
            {
                journal.PlantID = journalDto.PlantId;
            }
          
            return journal;
        }

        public static JournalDto MapDALToDto(Journal journal)
        {
            JournalDto dto = new JournalDto();
            dto.UserID = journal.UserID;
            dto.ID = journal.ID;
            dto.Name = journal.Name;
            dto.Notes = journal.Notes;
            if (journal.PlantID != null)
            {
                dto.PlantId = journal.PlantID ?? Guid.Empty;
            }


            return dto;
        }
    }
}