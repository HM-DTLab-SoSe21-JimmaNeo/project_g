﻿using System;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using SEIIApp.Server.DataAccess;
using SEIIApp.Server.Domain;
using System.Linq;

namespace SEIIApp.Server.Services
{

    public class ChapterDefinitionService
    {

        private DatabaseContext DatabaseContext { get; set; }
        private IMapper Mapper { get; set; }

        public ChapterDefinitionService(DatabaseContext db, IMapper mapper)
        {
            this.DatabaseContext = db;
            this.Mapper = mapper;
        }

        private IQueryable<ChapterDefinition> GetQueryableForChapterDefinitions()
        {
            return DatabaseContext.ChapterDefinitions
                .Include(chapter => chapter.ChapterElements);
        }

        /// <summary>
        /// Returns all chapters as an array, also contains the chapter elements of the chapters.
        /// </summary>
        public ChapterDefinition[] GetAllChapters()
        {
            return GetQueryableForChapterDefinitions().ToArray();
        }

        /// <summary>
        /// Returns the chapter with the given id, also includes the chapter elements of the chapter.
        /// </summary>
        public ChapterDefinition GetChapterById(int id)
        {
            ChapterDefinition chapterDefinition = GetQueryableForChapterDefinitions().Where(chapter => chapter.ChapterId == id).FirstOrDefault();
            return chapterDefinition;
        }

        /// <summary>
        /// Adds a chapter and returns it.
        /// </summary>
        public ChapterDefinition Addchapter(ChapterDefinition chapter)
        {
            chapter.CreationDate = DateTime.Now;
            chapter.ChangeDate = DateTime.Now;
            DatabaseContext.ChapterDefinitions.Add(chapter);
            DatabaseContext.SaveChanges();
            return chapter;
        }

        /// <summary>
        /// Updates a chapter and returns it.
        /// </summary>
        public ChapterDefinition UpdateChapter(ChapterDefinition chapter)
        {
            chapter.ChangeDate = DateTime.Now;
            DatabaseContext.ChapterDefinitions.Update(chapter);
            DatabaseContext.SaveChanges();
            return chapter;
        }

        /// <summary>
        /// Removes a chapter and all dependencies.
        /// </summary>
        public void RemoveChapter(ChapterDefinition chapter)
        {
            DatabaseContext.ChapterDefinitions.Remove(chapter);
            DatabaseContext.SaveChanges();
        }

    }

}
