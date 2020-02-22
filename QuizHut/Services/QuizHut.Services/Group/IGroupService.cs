﻿namespace QuizHut.Services.Group
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using QuizHut.Web.ViewModels.Groups;
    using QuizHut.Web.ViewModels.Participants;
    using QuizHut.Web.ViewModels.Quizzes;

    public interface IGroupService
    {
        Task<IList<T>> GetAllByCreatorIdAsync<T>(string id);

        Task<string> CreateAsync(string name, string creatorId);

        Task<GroupWithQuizzesViewModel> GetGroupModelAsync(string groupId, string creatorId, IList<QuizAssignViewModel> quizzes);

        Task<GroupDetailsViewModel> GetGroupDetailsModelAsync(string groupId);

        Task AssignQuizzesToGroupAsync(string groupId, List<string> quizzesIds);

        Task AssignParticipantsToGroupAsync(string groupId, IList<string> participantsIds);

        Task DeleteAsync(string groupId);

        Task UpdateNameAsync(string groupId, string newName);

        Task DeleteQuizFromGroupAsync(string groupId, string quizId);

        Task DeleteParticipantFromGroupAsync(string groupId, string participantId);

        Task<IList<QuizAssignViewModel>> FilterQuizzesThatAreNotAssignedToThisGroup(string qroupId, IList<QuizAssignViewModel> quizzes);

        Task<IList<ParticipantViewModel>> FilterParticipantsThatAreNotAssignedToThisGroup(string qroupId, IList<ParticipantViewModel> participants);
    }
}
