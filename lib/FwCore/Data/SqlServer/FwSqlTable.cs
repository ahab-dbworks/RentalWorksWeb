using FwCore.ConfigSections;
using System;

namespace FwCore.Data.SqlServer
{
    public class FwSqlTable
    {
        protected readonly DatabaseConfig _dbConfig;
        public FwSqlTable(DatabaseConfig dbConfig)
        {
            _dbConfig = dbConfig;   
        }

        public void Create()
        {
            beforeinsert();
            insert();
            afterinsert();
        }
        protected virtual void beforeinsert()
        {

        }
        protected virtual void insert()
        {
            // if not overriden then throw
            throw new NotSupportedException();
        }
        protected virtual void afterinsert()
        {

        }

        public void Update()
        {
            beforeupdate();
            update();
            afterupdate();
        }
        protected virtual void beforeupdate()
        {
            
        }
        protected virtual void update()
        {
            throw new NotSupportedException();
        }
        protected virtual void afterupdate()
        {

        }

        public void Delete()
        {
            beforedelete();
            delete();
            afterdelete();
        }
        protected virtual void beforedelete()
        {

        }
        protected virtual void delete()
        {
            throw new NotSupportedException();
        }
        protected virtual void afterdelete()
        {

        }
    }
}
