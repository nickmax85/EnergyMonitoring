﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Description;

namespace EnergyMonitoringWebAPI.Controllers
{
    public class RecordsController : ApiController
    {
        private EnergyMonitoringContext db = new EnergyMonitoringContext();


        // GET: api/Records
        public IEnumerable<Record> GetRecord()
        {


            using (EnergyMonitoringContext db = new EnergyMonitoringContext())
            {

                db.Configuration.LazyLoadingEnabled = false;

                return db.Records.Take(1000).ToList();

            }

        }

        // GET: api/Records/5
        [ResponseType(typeof(Record))]
        public async Task<IHttpActionResult> GetRecord(long id)
        {
            Record record = await db.Records.FindAsync(id);
            if (record == null)
            {
                return NotFound();
            }

            return Ok(record);
        }

        // GET: api/records/2
        [Route("api/records/{deviceId}/{startDate}/{endDate}")]
        public IEnumerable<Record> GetRecordsByDevice(int deviceId, DateTime startDate, DateTime endDate)
        {
            using (EnergyMonitoringContext db = new EnergyMonitoringContext())
            {

                db.Configuration.LazyLoadingEnabled = false;

                var items = db.Records
                     .Where(x => x.Sensor.DeviceID == deviceId)
                    .Where(x => DbFunctions.TruncateTime(x.CreateDate) >= DbFunctions.TruncateTime(startDate)
                    && DbFunctions.TruncateTime(x.CreateDate) <= DbFunctions.TruncateTime(endDate)).OrderBy(x => x.CreateDate)
                    .ToList();

                return items;
            }

        }

        // GET: api/records/2
        [Route("api/records/maxvalues/{startDate}/{endDate}")]
        public IEnumerable<object> GetMaxValues(DateTime startDate, DateTime endDate)
        {
            using (EnergyMonitoringContext db = new EnergyMonitoringContext())
            {
                var item = db.SpGetMaxValues(startDate, endDate).ToList();

                return item;
            }
        }


        //GET: api/records/count
        [Route("api/records/count")]
        public int GetRecordsCount()
        {
            using (EnergyMonitoringContext db = new EnergyMonitoringContext())
            {
                int count = db.Records.Count();

                var sql = "SELECT COUNT(*) FROM Record";
                var item = db.Database.SqlQuery<int>(sql).Single();

                return item;

            }
        }

        // GET: api/records/avg/{startDate}/{endDate}/{groupId}/{equipmentId}
        [Route("api/records/avg/{startDate}/{endDate}/{groupId}/{equipmentId}")]
        public IEnumerable<object> GetFilterRecordsAvg(DateTime startDate, DateTime endDate, int groupId, int equipmentId)
        {
            using (EnergyMonitoringContext db = new EnergyMonitoringContext())
            {
                var item = db.SpGetFilterRecordsAvg(startDate, endDate, groupId, equipmentId).ToList();

                return item;

            }

        }

        // GET:
        [Route("api/records/avg/flow/sum2/{year}/{weekday}/{startTime}/{endTime}")]
        public IEnumerable<object> GetAvgFlowSum2(int year, int weekday, TimeSpan startTime, TimeSpan endTime)
        {

            using (EnergyMonitoringContext db = new EnergyMonitoringContext())
            {
                var item = db.SpGetAvgFlowSum2(year, weekday, startTime, endTime).ToList();
                return item;
            }

        }

        // GET:
        [Route("api/records/avg/flow/equipments/{year}/{week}/{weekday}/{startTime}/{endTime}")]
        public IEnumerable<object> GetAvgFlowEquipments(int year, int week, int weekday, TimeSpan startTime, TimeSpan endTime)
        {


            using (EnergyMonitoringContext db = new EnergyMonitoringContext())
            {

                var item = db.SpGetAvgFlowEquipments(year, week, weekday, startTime, endTime).ToList();
                return item;

            }

        }

        // PUT: api/Records/5
        [ResponseType(typeof(void))]
        public async Task<IHttpActionResult> PutRecord(long id, Record record)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != record.RecordID)
            {
                return BadRequest();
            }

            db.Entry(record).State = EntityState.Modified;

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!RecordExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return StatusCode(HttpStatusCode.NoContent);
        }

        // POST: api/Records
        [ResponseType(typeof(Record))]
        public async Task<IHttpActionResult> PostRecord(Record record)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Records.Add(record);
            await db.SaveChangesAsync();

            return CreatedAtRoute("DefaultApi", new { id = record.RecordID }, record);
        }

        // DELETE: api/Records/5
        [ResponseType(typeof(Record))]
        public async Task<IHttpActionResult> DeleteRecord(long id)
        {
            Record record = await db.Records.FindAsync(id);
            if (record == null)
            {
                return NotFound();
            }

            db.Records.Remove(record);
            await db.SaveChangesAsync();

            return Ok(record);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool RecordExists(long id)
        {
            return db.Records.Count(e => e.RecordID == id) > 0;
        }
    }
}