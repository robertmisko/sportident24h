using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NewSpecialEvent.Models;
using SPORTident;

namespace NewSpecialEvent.Logic
{
    public class NewCardHandlerService : INewCardHandlerService
    {
        public event EventHandler<long> RunnerNotFound;
        public event EventHandler ResultError;
        public event EventHandler FinishPunchMissing;

        private IRunnerService runnerService;
        private ICardDataProcessor cardDataProcessor;
        private IResultPostProcessor resultPostProcessor;

        public NewCardHandlerService(IRunnerService runnerService, 
                                    ICardDataProcessor cardDataProcessor,
                                    IResultPostProcessor resultPostProcessor)
        {
            this.runnerService = runnerService;
            this.cardDataProcessor = cardDataProcessor;
            this.resultPostProcessor = resultPostProcessor;
        }

        public Result HandleNewCard(SportidentCard card)
        {
            Runner runner = runnerService.GetRunnerByCardId(card.SiidValue);
            Result processedResult = null;
            if (runner == null)
            {
                this.OnRunnerNotFound(card.SiidValue);
            }
            else
            {
                var newResult = cardDataProcessor.ProcessResult(card);
                if (card.FinishPunch.IsMissingOrEmpty)
                {
                    this.OnFinishTimeMissing();
                }

                if (newResult.Error != null)
                {
                    this.OnResultError();
                    var validationResult = resultPostProcessor.Validate(newResult);
                    newResult.Course = validationResult.Course;
                    newResult.IsValid = validationResult.IsValid;
                }
                else
                {
                    newResult.IsValid = true;
                }

                processedResult = resultPostProcessor.CheckDuplicates(newResult);
            }

            return processedResult;
        }


        protected virtual void OnRunnerNotFound(long cardnumber)
        {
            EventHandler<long> handler = this.RunnerNotFound;
            if (handler != null)
            {
                handler(this, cardnumber);
            }
        }

        protected virtual void OnResultError()
        {
            EventHandler handler = this.ResultError;
            if (handler != null)
            {
                handler(this, null);
            }
        }

        protected virtual void OnFinishTimeMissing()
        {
            if (this.FinishPunchMissing != null)
            {
                this.FinishPunchMissing(this, null);
            }
        }
    }
}
