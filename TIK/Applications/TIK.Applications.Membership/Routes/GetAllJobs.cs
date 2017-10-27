using Microsoft.Extensions.Logging;
using Akka.Actor;
using System.Threading.Tasks;
using System.Collections.Generic;
using TIK.Domain.Membership;
using TIK.Applications.Membership.Jobs;

namespace TIK.Applications.Membership.Routes
{
    public class GetAllJobs
    {
        private ILogger<GetAllJobs> Logger { get; set; }
        private IActorRef JobsActor { get; set; }

        public GetAllJobs(JobsActorProvider provider, ILogger<GetAllJobs> logger)
        {
            this.Logger = logger;
            this.JobsActor = provider.Get();
        }

        public async Task<IEnumerable<Job>> Execute() {
            Logger.LogInformation("Requesting all jobs");
            return await this.JobsActor.Ask<IEnumerable<Job>>(
                new JobsActor.GetAllJobs()
            );
        }
    }
}
