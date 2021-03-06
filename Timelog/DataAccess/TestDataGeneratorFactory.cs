﻿using Timelog.Common;
using Timelog.TestData;

namespace Timelog.DataAccess
{
    public class TestDataGeneratorFactory : IDataGeneratorFactory
    {
        public IDataGenerator Build(TimeLogContext context)
        {
            return new TestDataGenerator(new DataSeeder(context));
        }
    }
}