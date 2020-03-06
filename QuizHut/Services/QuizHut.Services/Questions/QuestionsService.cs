﻿namespace QuizHut.Services.Questions
{
    using System.Linq;
    using System.Threading.Tasks;

    using Microsoft.EntityFrameworkCore;
    using QuizHut.Data.Common.Repositories;
    using QuizHut.Data.Models;


    public class QuestionsService : IQuestionsService
    {
        private readonly IDeletableEntityRepository<Question> repository;
        private readonly IDeletableEntityRepository<Quiz> quizRepository;

        public QuestionsService(IDeletableEntityRepository<Question> repository, IDeletableEntityRepository<Quiz> quizRepository)
        {
            this.repository = repository;
            this.quizRepository = quizRepository;
        }

        public async Task<string> AddNewQuestionAsync(string quizId, string quizText)
        {
            var quiz = await this.quizRepository.AllAsNoTracking().Select(x => new
            {
                x.Id,
                Questions = x.Questions.ToList(),
            }).FirstOrDefaultAsync(x => x.Id == quizId);

            var question = new Question
            {
                Number = quiz.Questions.Count() + 1,
                Text = quizText,
                QuizId = quizId,
            };

            await this.repository.AddAsync(question);
            await this.repository.SaveChangesAsync();

            return question.Id;
        }

        public async Task DeleteQuestionByIdAsync(string id)
        {
            var question = await this.repository.AllAsNoTracking().FirstOrDefaultAsync(x => x.Id == id);
            this.repository.Delete(question);
            await this.repository.SaveChangesAsync();
            await this.UpdateAllQuestionsInQuizNumeration(question.QuizId);
        }

        public async Task UpdateAllQuestionsInQuizNumeration(string quizId)
        {
            var count = 0;

            var questions = this.repository
              .AllAsNoTracking()
              .Where(x => x.QuizId == quizId)
              .OrderBy(x => x.Number);

            foreach (var question in questions)
            {
                question.Number = ++count;
                this.repository.Update(question);
            }

            await this.repository.SaveChangesAsync();
        }

        public async Task Update(string id, string text)
        {
            var question = await this.repository.AllAsNoTracking().FirstOrDefaultAsync(x => x.Id == id);
            question.Text = text;
            this.repository.Update(question);
            await this.repository.SaveChangesAsync();
        }
    }
}