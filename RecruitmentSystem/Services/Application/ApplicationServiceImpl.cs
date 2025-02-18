using DocumentFormat.OpenXml;
using DocumentFormat.OpenXml.InkML;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.EntityFrameworkCore;
using RecruitmentSystem.Data;
using RecruitmentSystem.dto;
using RecruitmentSystem.Models;

namespace RecruitmentSystem.Services.Application;

public class ApplicationServiceImpl : IApplicationService
{
    private readonly ApplicationDbContext _context;

    public ApplicationServiceImpl(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<ApplicationModel> getApplication(int candidateId, int positionId)
    {
        return await _context.Applications.FirstOrDefaultAsync(a => a.fk_candidate_id == candidateId && a.fk_position_id == positionId);
    }

    public async Task<bool> applyApplication(List<int> ids, int positionId)
    {
        try
        {
            var existingApplications = await _context.Applications.Where(a => a.fk_position_id == positionId).ToListAsync();
            var existingsIds = existingApplications.Select(a => a.fk_candidate_id);
            var newApplications = ids.Where(id => !existingsIds.Contains(id)).Select(id => new ApplicationModel { fk_candidate_id = id, fk_position_id = positionId });
            await _context.Applications.AddRangeAsync(newApplications);
            await _context.SaveChangesAsync();
            return true;
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
            return false;
        }
    }

    public async Task<List<ApplicationModel>> getAllApplicationOfCandidate(int candidateId)
    {
        try
        {
            List<ApplicationModel> applications = await _context.Applications.Where(a => a.fk_candidate_id == candidateId).ToListAsync();
            return applications;
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
            return [];
        }
    }

    public async Task<ApplicationModel> getApplicationById(int applicationId)
    {
        return await _context.Applications.FindAsync(applicationId);
    }

    public async Task<bool> removeApplication(int applicationId)
    {
        try
        {
            ApplicationModel application = await getApplicationById(applicationId);
            if (application == null)
            {
                throw new Exception("No Application is registered with this id");
            }
            await _context.Applications.Where(a => a.pk_application_id == applicationId).ExecuteDeleteAsync();
            return true;
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
            return false;
        }
    }

    public async Task<bool> updateApplication(int applicationId, ApplicationModel application)
    {
        try
        {
            ApplicationModel is_application_exist = await getApplicationById(applicationId);
            if (is_application_exist == null)
            {
                throw new Exception("No Application is registered with this id");
            }
            _context.Entry(is_application_exist).CurrentValues.SetValues(application);
            await _context.SaveChangesAsync();
            return true;
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
            return false;
        }
    }

    public async Task<bool> applicationOnHold(int applicationId)
    {
        try
        {
            ApplicationModel is_application_exist = await getApplicationById(applicationId);
            if (is_application_exist == null)
            {
                throw new Exception("No Application is registered with this id");
            }
            if (is_application_exist.applicationStatus == ApplicationStatus.on_hold)
            {
                Console.WriteLine("Already On Hold");
                return true;
            }
            is_application_exist.applicationStatus = ApplicationStatus.on_hold;
            await _context.SaveChangesAsync();
            return true;
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
            return false;
        }
    }

    public async Task<bool> changeApplication(int applicationId, JsonPatchDocument<ApplicationModel> application)
    {
        try
        {
            ApplicationModel is_application_exist = await getApplicationById(applicationId);
            if (is_application_exist == null)
            {
                throw new Exception("Application Do not exist with this application id");
            }
            application.ApplyTo(is_application_exist);
            await _context.SaveChangesAsync();
            return true;
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
            return false;
        }
    }

    public async Task<List<ApplicationModel>> getAllApplicationByPositionId(int positionId)
    {
        try
        {
            List<ApplicationModel> applications = await _context.Applications.Where(a => a.fk_position_id == positionId).ToListAsync();
            return applications;
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
            return null;
        }
    }

    public async Task<List<ApplicationPositiondto>> getAllByCandidateNotAppliedPosition(int positionId)
    {
        try
        {
            List<ApplicationPositiondto> applications = new List<ApplicationPositiondto>();
           var query = @"select * from Candidate where pk_candidate_id not in (select fk_candidate_id from Applications where fk_position_id = {0})";
           var result = await _context.Candidate.FromSqlRaw(query,positionId).ToListAsync();
            foreach (var r in result)
            {
                ApplicationPositiondto app = new ApplicationPositiondto();

                app.candidate_email = r.candidate_email;
                app.candidate_name = r.candidate_name;
                app.pk_candidate_id = r.pk_candidate_id;

                applications.Add(app);
            }
            return applications;
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
            return null;
        }
    }
}