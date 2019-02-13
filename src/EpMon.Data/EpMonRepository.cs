﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using EpMon.Data.Entities;
using Microsoft.EntityFrameworkCore;

namespace EpMon.Data
{
    public class EpMonRepository
    {
        public Endpoint GetEndpoint(int endpointId)
        {
            using (var db = new EpMonContext())
            {
                var endpoint = db.Endpoints.Include("Stats").FirstOrDefault(q => q.Id == endpointId);

                return endpoint;
            }
        }

        public IEnumerable<Endpoint> GetEndpoints()
        {
            using (var db = new EpMonContext())
            {
                var endpoints = db.Endpoints.ToList();

                foreach (var endpoint in endpoints)
                {
                    
                    endpoint.Stats = new List<EndpointStat>();
                    var stat = GetLastStat(endpoint.Id);
                    endpoint.Stats.Add(stat);
                }

                return endpoints;
            }
        }

        public void AddStat(EndpointStat stat)
        {
            using (var db = new EpMonContext())
            {
                var endpoint = db.Endpoints.Include("Stats").FirstOrDefault(q => q.Id == stat.EndpointId);

                endpoint?.Stats.Add(stat);
                db.SaveChanges();
            }
        }

        public void CleanStats(int daysToKeep)
        {
            using (var db = new EpMonContext())
            {
                var compareWith = DateTime.UtcNow.AddDays(-daysToKeep);
                var statsToRemove = db.EndpointStats.Where(x => (x.TimeStamp <= compareWith));
                db.EndpointStats.RemoveRange(statsToRemove);
                db.SaveChanges();
            }
        }

        public IEnumerable<EndpointStat> GetStats(int endpointId)
        {
            using (var db = new EpMonContext())
            {
                var endpoint = db.Endpoints.Include("Stats").FirstOrDefault(q => q.Id == endpointId);

                return endpoint?.Stats.OrderByDescending(x => x.TimeStamp);                
            }
        }
        
        public EndpointStat GetLastStat(int endpointId)
        {
            using (var db = new EpMonContext())
            {
                var stat = db.EndpointStats.Include("Endpoint").Where(q => q.Endpoint.Id == endpointId).OrderByDescending(x => x.TimeStamp).FirstOrDefault();

                return stat;
            }
        }

        public int AddEndpoint(string url, int checkInterval, string tags)
        {
            using (var db = new EpMonContext())
            {
                var result = db.Endpoints.Add(new Endpoint
                    {Url = url, CheckInterval = checkInterval, Tags = tags, CheckType = CheckType.AvailabilityCheck});
                db.SaveChanges();

                return result.Entity.Id;
            }
        }

        public void CustomSeed()
        {
            using (var context = new EpMonContext())
            {
                var testEndpoint = context.Endpoints.FirstOrDefault(b => b.Url == @"http:\\blog.jeroenmaes.eu");
                if (testEndpoint == null)
                {
                    context.Endpoints.Add(new Endpoint
                    {
                        CheckInterval = 5,
                        CheckType = CheckType.AvailabilityCheck,
                        Tags = "Personal",
                        Url = @"http:\\blog.jeroenmaes.eu"
                    });
                }
                context.SaveChanges();
            }
        }
    }
}
