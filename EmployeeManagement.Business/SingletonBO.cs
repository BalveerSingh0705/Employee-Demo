namespace EmployeeManagement.Business
{



    /// <summary>
    /// The use of this singleton is to maintain single instance of BO
    /// </summary>
    /// <typeparam name="T">Type T is any BO class which used to get/create instance.</typeparam>
    public sealed class SingletonBO<T> where T : class, new()
        {
            private static volatile T _instance;
            private static readonly object _syncRoot = new object();

            // To stop creating new instance using the class.
            private SingletonBO()
            {
            }

            /// <summary>
            /// The use of Instance to create/use instance of "T".
            /// </summary>
            public static T Instance
            {
                get
                {
                    // If instance is already available don't create new just return existing one.
                    if (_instance != null) return _instance;

                    // To handle multithread access
                    lock (_syncRoot)
                    {
                        // Chek if instance is null or not.
                        if (_instance == null)
                        {
                            // Create new instance if it's null
                            _instance = new T();
                        }
                    }

                    return _instance;
                }
            }

            //#region IDisposable Support

            //private bool _disposed; // To detect redundant calls

            //// Implement IDisposable.
            //// Do not make this method virtual.
            //// A derived class should not be able to override this method.
            //public void Dispose()
            //{
            //    Dispose(true);
            //    // This object will be cleaned up by the Dispose method.
            //    // Therefore, you should call GC.SupressFinalize to
            //    // take this object off the finalization queue
            //    // and prevent finalization code for this object
            //    // from executing a second time.
            //    GC.SuppressFinalize(this);
            //}

            //// Dispose(bool disposing) executes in two distinct scenarios.
            //// If disposing equals true, the method has been called directly
            //// or indirectly by a user's code. Managed and unmanaged resources
            //// can be disposed.
            //// If disposing equals false, the method has been called by the
            //// runtime from inside the finalizer and you should not reference
            //// other objects. Only unmanaged resources can be disposed.
            //private void Dispose(bool disposing)
            //{
            //    // Check to see if Dispose has already been called.
            //    if (_disposed) return;
            //    // If disposing equals true, dispose all managed
            //    // and unmanaged resources.
            //    if (disposing)
            //    {
            //        _instance = null;
            //        // Dispose managed resources.
            //    }

            //    // Call the appropriate methods to clean up
            //    // unmanaged resources here.
            //    // If disposing is false,
            //    // only the following code is executed.
            //    // Note disposing has been done.
            //    _disposed = true;
            //}

            //#endregion




        }
}
