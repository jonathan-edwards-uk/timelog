﻿using System.Collections.Generic;
using Timelog.Model;

namespace Timelog.DataService.Interface
{
    public interface ITimeEntryDataService
    {
        TimeEntry GetById(int id);
        void Add(TimeEntry timeEntry);
        TimeEntry Put(TimeEntry timeEntry);
        bool Delete(int id);
    }
}