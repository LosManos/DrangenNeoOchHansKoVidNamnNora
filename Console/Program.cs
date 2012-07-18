using System;
using System.Collections.Generic;
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


            Console.WriteLine();
            Console.Write("Press any key...");
            Console.ReadKey();
        }
    }
}
