﻿using Microsoft.Practices.Unity;
using System;
using System.Collections.Generic;
using System.Linq;
using Timelog.DataService.Interface;
using Timelog.Model;

namespace Timelog.DataAccess.Repositories
{
    public class TagTreeRepository : ITagTreeRepository, IDisposable
    {
        private IUnityContainer _unityContainer;
        private TimeLogContext _db;

        public TagTreeRepository(IUnityContainer unityContainer, TimeLogContext db)
        {
            _unityContainer = unityContainer;
            _db = db;
        }

        private IEnumerable<TagTree> GetAllInternal()
        {
            var result = _db.TagTrees.IncludeMultiple(x => x.Tag, y => y.RelatedTagTree);
            //foreach (var tagTree in result)
            //{
            //    var related = tagTree.RelatedTagTree;
            //    while (related != null)
            //    {
            //        related = related.RelatedTagTree;
            //    }
            //}

            return result;
        }

        public IEnumerable<TagTree> GetAll()
        {
            //var result = db.TagTrees.Include(x => x.Tag);
            //var result = db.TagTrees.ToList();

            var result = GetAllInternal();

            return result;
        }
        public TagTree GetById(int id)
        {
            //var result = db.TagTrees.IncludeMultiple(x => x.Tag, y => y.RelatedTagTree).FirstOrDefault(p => p.Id == id);
            //var result = db.TagTrees.IncludeMultiple(x=>x.Tag, y=>y.RelatedTagTree).FirstOrDefault(p => p.Id == id);
            //var result = db.TagTrees.Where(x => x.Id == id).Include(x => x.Tag).ToList().FirstOrDefault();
            //var result = db.TagTrees.Where(x => x.Id == id).IncludeMultiple(x => x.Tag, y=>y.RelatedTagTree).ToList().FirstOrDefault();
            
            //var qryTrees = from q in db.TagTrees
            //                    where q.Id == id
            //                    select new
            //                    {
            //                        TagTree = q,
            //                        Id = q.Id,
            //                        Items = q.RelatedTagTree,
            //                        Bob = q.Tag
            //                    };

            //var result = qryTrees.FirstOrDefault().TagTree;

            var result = GetAllInternal();

            return result.FirstOrDefault(x => x.Id == id);
        }

        public void Add(TagTree tagTree)
        {
            _db.TagTrees.Add(tagTree);
            _db.SaveChanges();
        }

        protected void Dispose(bool disposing)
        {
            if (disposing)
            {
                if (_db != null)
                {
                    _db.Dispose();
                    _db = null;
                }
            }
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}
