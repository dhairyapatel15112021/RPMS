using RecruitmentSystem.Data;

namespace RecruitmentSystem.Services.InterviewFeedback;

public class InterviewFeedbackServiceImpl : IInterviewFeedbackService {

    private readonly ApplicationDbContext _context;

    public InterviewFeedbackServiceImpl(ApplicationDbContext context){
        _context = context;
    }
}