﻿using Timelog.Model;

namespace Timelog.Common.Interface
{
    public interface ITagRepository : IGetAllRepository<Tag>
    {
        Tag Update(Tag tag);
    }
}