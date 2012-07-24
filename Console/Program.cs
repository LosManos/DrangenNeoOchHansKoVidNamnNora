using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TheConsole
{
    class Program
    {
        static void Main(string[] args)
        {

            var repositoryBL = new BL.Repository();
            repositoryBL.Setup();

            var workerBL = new BL.Worker();

            Console.WriteLine("Adding new worker.");
            var newWorker = workerBL.AddWorker("My new worker " + DateTime.Now.ToShortTimeString());
            Console.WriteLine(string.Format("New Worker id={0}, name={1}.", newWorker.ID, newWorker.Name));

            Console.WriteLine("Getting the newly created worker.");
            var worker = workerBL.Get(newWorker.ID);
            Console.WriteLine("The worker's name={0}.", worker.Name);

            Console.WriteLine("Getting all workers.");
            var workerList = workerBL.GetAll();
            foreach (var w in workerList)
            {
                Console.WriteLine(string.Format( "ID:{0},Name:{1}", w.ID, w.Name));
            }

            Console.WriteLine("Updating the newly created worker.");
            worker.Name = "New worker [" + DateTime.Now.ToShortTimeString() + "]";
            var updatedWorker = workerBL.Update( worker );
            Console.WriteLine("Worker updated with name = " + worker.Name + " resulted in " + updatedWorker.Name);

            Console.WriteLine("Deleting the node again.");
            workerBL.Delete(worker.ID);
            workerList = workerBL.GetAll();
            foreach (var w in workerList)
            {
                Console.WriteLine(string.Format("ID:{0},Name:{1}", w.ID, w.Name));
            }

            ManipulateChunk(workerList.First());


            Console.WriteLine();
            Console.Write("Press any key...");
            Console.ReadKey();
        }

        private static void ManipulateChunk(BL.DTO.Worker worker)
        {
            Console.WriteLine("Commencing Chunks");
            var chunkBL = new BL.Chunk();

            var chunk1 = chunkBL.Add(DateTimeOffset.Now, DateTimeOffset.Now.AddMinutes(30), TimeSpan.Zero, worker.ID);
            Console.WriteLine(string.Format("Chunk.ID {0} for Worker.ID{1} has been created with start:{2} and stop:{3} and duration:{4}.", chunk1.ID, worker.ID, chunk1.Start, chunk1.Stop, chunk1.Duration));
            var chunk2 = chunkBL.Add(DateTimeOffset.Now, DateTimeOffset.Now.AddMinutes(30), new TimeSpan( 0,30,0), worker.ID);
            Console.WriteLine(string.Format("Chunk.ID {0} for Worker.ID{1} has been created with start:{2} and stop:{3} and duration:{4}.", chunk2.ID, worker.ID, chunk2.Start, chunk2.Stop, chunk2.Duration));

            Console.WriteLine("Getting what we have created.");
            var chunk3 = chunkBL.Get(chunk2.ID);
            Debug.Assert(chunk2.ID == chunk3.ID );
            Debug.Assert(chunk2.Start == chunk3.Start);
            Debug.Assert(chunk2.Stop == chunk3.Stop);
            Debug.Assert(chunk2.Duration == chunk3.Duration);

            Console.WriteLine(string.Format("Updating Chunk.ID:{0} with 20 minutes more.", chunk2.ID));
            chunk2.Stop += new TimeSpan(0, 20, 0);
            chunk2.Duration += new TimeSpan(0, 20, 0);
            chunk2 = chunkBL.Update(chunk2);
            Console.WriteLine(string.Format("Resulted in Stop:{0} and Duration:{1}", chunk2.Stop, chunk2.Duration));

            Console.WriteLine(string.Format("Deleting Chunk with ID:{0}:", chunk2.ID));
            chunkBL.Delete(chunk2);
            var exceptionThrown = false;
            try
            {
                var x = chunkBL.Get(chunk2.ID);
            }
            catch (NullReferenceException)
            {
                exceptionThrown = true;
                Console.WriteLine( "An exception was thrown since we tried to retrieve a Chunk that was deleted.");
            }
            Debug.Assert(exceptionThrown);
        }
    }
}
