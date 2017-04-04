namespace NewSpecialEvent.Logic
{
    using System;
    using System.Threading;
    using System.Threading.Tasks;
    using System.Threading.Tasks.Dataflow;
    using Dao.Interface;
    using NewSpecialEvent.Models;
    using SPORTident;
    /// <summary>
    /// Processes new read-outs and computes the Result.
    /// </summary>
    public class CardDataProcessor : ICardDataProcessor
    {
        private readonly IResultDataAccess resultDao;
        private readonly IResultCalculator resultCalculator;

        /// <summary>
        /// Initializes a new instance of the <see cref="CardDataProcessor"/> class.
        /// </summary>
        /// <param name="resultDao">The result DAO.</param>
        /// <param name="resultCalculator">The result calculator.</param>
        public CardDataProcessor(IResultDataAccess resultDao, 
                                 IResultCalculator resultCalculator)
        {
            this.resultDao = resultDao;
            this.resultCalculator = resultCalculator;
        }

        /// <summary>
        /// Starts the processing.
        /// </summary>
        public Result ProcessResult(SportidentCard card)
        {
            if (card == null)
            {
                throw new ArgumentNullException("card");
            }

            var runner = this.resultDao.GetRunnerByCardId(card.SiidValue);
            var result = this.resultCalculator.GetResult(card, runner);
            this.resultDao.AddOrUpdate(result);

            return result;
        }
    }
}