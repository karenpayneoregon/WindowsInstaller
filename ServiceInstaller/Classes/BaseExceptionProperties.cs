using System;

namespace ServiceInstaller.Classes
{
    public class BaseExceptionProperties
    {
        protected bool mHasException;
        /// <summary>
        /// Indicate the last operation thrown an 
        /// exception or not.
        /// </summary>
        /// <returns></returns>
        public bool HasException => mHasException;

        protected Exception mLastException;
        /// <summary>
        /// Provides access to the last exception thrown
        /// </summary>
        /// <returns></returns>
        public Exception LastException => mLastException;
        /// <summary>
        /// If you don't need the entire exception as in 
        /// LastException this provides just the text of the exception
        /// </summary>
        /// <returns></returns>
        public string LastExceptionMessage => LastException.Message;

        /// <summary>
        /// Indicate for return of a function if there was an 
        /// exception thrown or not.
        /// </summary>
        /// <returns></returns>
        public bool IsSuccessFul => !HasException;
    }
}