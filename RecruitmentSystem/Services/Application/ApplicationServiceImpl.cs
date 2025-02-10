using DocumentFormat.OpenXml;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.EntityFrameworkCore;
using RecruitmentSystem.Data;
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

    public async Task<bool> applyApplication(ApplicationModel application)
    {
        try
        {
            ApplicationModel is_application_exist = await getApplication(application.fk_candidate_id, application.fk_position_id);
            if (is_application_exist != null)
            {
                throw new Exception("Already Applied for this position");
            }
            await _context.Applications.AddAsync(application);
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
}