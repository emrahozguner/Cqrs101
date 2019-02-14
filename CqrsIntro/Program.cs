using CqrsIntro;
using CqrsIntro.Aggregate;
using CqrsIntro.Command;
using CqrsIntro.Context;
using CqrsIntro.IoC;
using CqrsIntro.Query;
using CqrsIntro.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;

namespace CqrsIntro
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            DataBaseInitializer<TaskContext>.InitializedDatabase();

            IContainer container = new SimpleIocContainer();

            BootStrapper.Configure(container);

            ICommandDispatcher commandDispatcher = container.Resolve<ICommandDispatcher>();

            IQueryDispatcher queryDispatcher = container.Resolve<IQueryDispatcher>();

            var createCommand = new CreateTaskCommand { Title = "CQRS Örneği", UserName = "Emrah özgüner", IsCompleted = false, CreatedOn = DateTime.Now, UpdatedOn = DateTime.Now };

            commandDispatcher.Execute(createCommand);

            var getTasksQuery = new GetTasksQuery();

            getTasksQuery.Predicate = (t) => t.IsCompleted == false;

            IQueryable<Task> tasks = queryDispatcher.Query<GetTasksQuery, IQueryable<Task>>(getTasksQuery);

            Console.WriteLine("Bitmemiş tasklar getiriliyor.");

            foreach (var task in tasks.ToList())
            {
                Console.WriteLine(task);
            }

            //var lastTask = tasks.ToList().LastOrDefault();

            var changeCommand = new ChangeTaskStatusCommand { TaskId = 9, IsCompleted = true, UpdatedOn = DateTime.Now.AddMinutes(5) };
            //var changeTaskCommand = new ChangeTaskCommand { TaskId = 5, UserName = "Berat Ozguner", UpdatedOn = DateTime.Now.AddMinutes(5) };

            commandDispatcher.Execute(changeCommand);

            GetTasks(queryDispatcher, (t) => t.IsCompleted == true);

            Console.ReadLine();
        }

        public static void GetTasks(IQueryDispatcher queryDispatcher, Expression<Func<Task, bool>> predicate = null)
        {
            var getTasksQuery = new GetTasksQuery();

            getTasksQuery.Predicate = predicate;

            IQueryable<Task> tasks = queryDispatcher.Query<GetTasksQuery, IQueryable<Task>>(getTasksQuery);

            Console.WriteLine("Güncelleme sonrası Bitmiş tasklar getiriliyor.");

            foreach (var task in tasks.ToList())
            {
                Console.WriteLine(task);
            }
        }
    }
}