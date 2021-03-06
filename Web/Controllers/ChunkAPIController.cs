﻿using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace Web.Controllers
{
    public class ChunkAPIController : ApiController
    {
        // GET api/chunkapi
        public Models.ChunksModel Get()
        {
            //  HACK:   test data.
            var ret = Models.ChunksModel.Create(
                -1, 
                "worker x ",
                new List<Models.ChunkModel>(){
                    Models.ChunkModel.Create( 1, DateTime.Now, null, TimeSpan.FromMinutes(10 ), 1 ), 
                    Models.ChunkModel.Create( 2, DateTime.Now.AddMinutes(10), DateTime.Now.AddMinutes(20), null, 2 )
                }
            );
            return ret;
        }

        // GET api/chunkapi/5
        public Models.ChunksModel Get(int workerID)
        {
            var workerBL = new BL.Worker();
            var worker = workerBL.Get(workerID);
            Debug.Assert( worker.ID == workerID );

            var chunkBL = new BL.Chunk();
            var chunks = chunkBL.GetByWorker(worker);

            var ret = Models.ChunksModel.Create(worker.ID, worker.Name,
                chunks.Select( c => Models.ChunkModel.Create( c, worker.ID )).ToList()
                );

            return ret;
        }

        // POST api/chunkapi
        public Models.ChunkModel Post(Models.ChunkModel value)
        {
            Trace.Write(value.ID);

            var bl = new BL.Chunk();
            var newChunk = bl.Add(value.Start, value.Stop, value.Duration, value.OwningWorkerID);
            return Models.ChunkModel.Create(newChunk,value.OwningWorkerID);
        }

        // PUT api/chunkapi/5
        public void Put(int id, string value)
        {
        }

        // DELETE api/chunkapi/5
        public void Delete(int id)
        {
            var chunkBL = new BL.Chunk();
            var chunk = chunkBL.Get(id);
            chunkBL.Delete(chunk);
        }
    }
}
