using System;
using System.Collections.Generic;

namespace HiringProcess
{
    public class JobRequest
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public bool IsApproved { get; set; }
    }

    public class Candidate
    {
        public string Name { get; set; }
        public string Resume { get; set; }
        public bool PassedPrimaryInterview { get; set; }
        public bool PassedTechnicalInterview { get; set; }
    }

    public class HiringProcess
    {
        private List<JobRequest> jobRequests = new List<JobRequest>();
        private List<Candidate> candidates = new List<Candidate>();

        public void CreateJobRequest(string title, string description)
        {
            var jobRequest = new JobRequest
            {
                Title = title,
                Description = description,
                IsApproved = false
            };

            Console.WriteLine($"Job request '{title}' created by Department Head.");
            jobRequests.Add(jobRequest);
        }

        public void ReviewJobRequest(int requestId)
        {
            if (requestId < 0 || requestId >= jobRequests.Count)
            {
                Console.WriteLine("Invalid job request ID.");
                return;
            }

            var jobRequest = jobRequests[requestId];
            jobRequest.IsApproved = true;
            Console.WriteLine($"Job request '{jobRequest.Title}' approved by HR Department.");
        }

        public void PublishJobPosting(int requestId)
        {
            if (requestId < 0 || requestId >= jobRequests.Count || !jobRequests[requestId].IsApproved)
            {
                Console.WriteLine("Job request not found or not approved.");
                return;
            }

            Console.WriteLine($"Job posting for '{jobRequests[requestId].Title}' published.");
        }

        public void SubmitApplication(string name, string resume)
        {
            var candidate = new Candidate
            {
                Name = name,
                Resume = resume,
                PassedPrimaryInterview = false,
                PassedTechnicalInterview = false
            };

            Console.WriteLine($"Application submitted by {name}.");
            candidates.Add(candidate);
        }

        public void FilterApplications()
        {
            Console.WriteLine("HR Department filtering applications...");
            foreach (var candidate in candidates)
            {
                Console.WriteLine($"Candidate {candidate.Name} is invited for an interview.");
            }
        }

        public void ConductPrimaryInterview(int candidateId)
        {
            if (candidateId < 0 || candidateId >= candidates.Count)
            {
                Console.WriteLine("Invalid candidate ID.");
                return;
            }

            candidates[candidateId].PassedPrimaryInterview = true;
            Console.WriteLine($"Primary interview passed by {candidates[candidateId].Name}.");
        }

        public void ConductTechnicalInterview(int candidateId)
        {
            if (candidateId < 0 || candidateId >= candidates.Count || !candidates[candidateId].PassedPrimaryInterview)
            {
                Console.WriteLine("Candidate not eligible for technical interview.");
                return;
            }

            candidates[candidateId].PassedTechnicalInterview = true;
            Console.WriteLine($"Technical interview passed by {candidates[candidateId].Name}.");
        }

        public void OfferJob(int candidateId)
        {
            if (candidateId < 0 || candidateId >= candidates.Count ||
                !candidates[candidateId].PassedPrimaryInterview || !candidates[candidateId].PassedTechnicalInterview)
            {
                Console.WriteLine("Candidate not eligible for job offer.");
                return;
            }

            Console.WriteLine($"Job offer sent to {candidates[candidateId].Name}.");
        }

        public void ConfirmOffer(int candidateId)
        {
            if (candidateId < 0 || candidateId >= candidates.Count)
            {
                Console.WriteLine("Invalid candidate ID.");
                return;
            }

            Console.WriteLine($"{candidates[candidateId].Name} accepted the job offer.");
        }

        public void AddToDatabase(int candidateId)
        {
            if (candidateId < 0 || candidateId >= candidates.Count)
            {
                Console.WriteLine("Invalid candidate ID.");
                return;
            }

            Console.WriteLine($"{candidates[candidateId].Name} added to employee database.");
        }

        public void NotifyITDepartment()
        {
            Console.WriteLine("IT Department notified to setup workspace.");
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            var hiringProcess = new HiringProcess();

            hiringProcess.CreateJobRequest("Software Engineer", "Develop and maintain software applications.");
            hiringProcess.ReviewJobRequest(0);
            hiringProcess.PublishJobPosting(0);
            hiringProcess.SubmitApplication("Alice", "Alice's Resume");
            hiringProcess.SubmitApplication("Bob", "Bob's Resume");
            hiringProcess.FilterApplications();
            hiringProcess.ConductPrimaryInterview(0);
            hiringProcess.ConductTechnicalInterview(0);
            hiringProcess.OfferJob(0);
            hiringProcess.ConfirmOffer(0);
            hiringProcess.AddToDatabase(0);
            hiringProcess.NotifyITDepartment();
        }
    }
}