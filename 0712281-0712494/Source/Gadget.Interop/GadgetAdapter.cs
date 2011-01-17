using System;
using System.Collections;
using System.Globalization;
using System.IO;
using System.Reflection;
using System.Runtime.InteropServices;

namespace Gadget.Interop
{
    /// <summary>
    /// Interface required COM registration.  This interface outlines methods
    /// used to create and destroy managed .NET types
    /// </summary>
    [ComVisible(true),
    GuidAttribute("618ACBAF-B4BC-4165-8689-A0B7D7115B05"),
    InterfaceType(ComInterfaceType.InterfaceIsDual)]
    public interface IGadgetInterop
    {
        object LoadType(string assemblyFullPath, string className);
        object LoadTypeWithParams(string assemblyFullPath, string className, bool preserveParams);
        void AddConstructorParam(object parameter);
        void UnloadType(object typeToUnload);
    }

    /// <summary>
    /// GadgetAdapter is the starting point for loading and unloading .NET assemblies
    /// from javascript or any COM-based environment.
    /// </summary>
    [ComVisible(true),
    GuidAttribute("89BB4535-5AE9-43a0-89C5-19B4697E5C5E"),
    ProgId("GadgetInterop.GadgetAdapter"),
    ClassInterface(ClassInterfaceType.None)]
    public class GadgetAdapter : IGadgetInterop
    {
        private ArrayList paramList = new ArrayList(); // Array list to hold constructor parameters

        #region IGadgetInterop Members
        /// <summary>
        /// Adds an object to be passed to a class' constructor.  Values must be passed to 
        /// this method in the same order of the constructor's arguments.
        /// </summary>
        /// <param name="parameter">Constructor agrument value</param>
        public void AddConstructorParam(object parameter)
        {
            paramList.Add(parameter);
        }
        /// <summary>
        /// Creates an instance of the specified type.  Constructor parameters are used when
        /// calling this method if they exist, but are forcibly cleared after object creation.
        /// </summary>
        /// <param name="assemblyFullPath">Full path to and name of the assembly to load</param>
        /// <param name="className">Full namespace and class name of the type to create</param>
        /// <returns>Instance of an object represented by the className argument</returns>
        public object LoadType(string assemblyFullPath, string className)
        {
            try
            {
                return LoadTypeWithParams(assemblyFullPath, className, false);
            }
            catch (Exception ex)
            {
                // Javascript-friendly exception
                throw new Exception(ex.Message);
            }
        }
        /// <summary>
        /// Creates an instance of the specified type.  Any constructor parameters are cleared
        /// after object creation if the preserveParams argument is "flase."
        /// </summary>
        /// <param name="assemblyFullPath">Full path to and name of the assembly to load</param>
        /// <param name="className">Full namespace and class name of the type to create</param>
        /// <param name="preserveParams">Clears the constructor parameters if false</param>
        /// <returns>Instance of an object represented by the className argument</returns>
        public object LoadTypeWithParams(string assemblyFullPath, string className, bool preserveParams)
        {
            if (string.IsNullOrEmpty(assemblyFullPath) || !File.Exists(assemblyFullPath))
            {
                throw new Exception("The specified assembly was not found: " + assemblyFullPath);
            }

            try
            {
                // Load the assembly
                Assembly assembly = Assembly.LoadFile(assemblyFullPath);

                object[] arguments = null;

                // Copy any parameters to the arguments array
                if (paramList != null && paramList.Count > 0)
                {
                    arguments = new object[paramList.Count];
                    paramList.CopyTo(arguments);
                }

                // Create an instance of the specified type
                BindingFlags bindings = BindingFlags.CreateInstance | BindingFlags.Instance | BindingFlags.Public;
                object loadedType = assembly.CreateInstance(className, false, bindings, null, arguments, CultureInfo.InvariantCulture, null);

                // The class must support IDisposable
                if (!(loadedType is IDisposable))
                {
                    throw new Exception("Loaded types must implement IDisposable");
                }

                // Clear the constructor parameters if they're no longer needed.  The caller
                // may choose to keep the parameters in tact if they're creating multiple
                // instances of the same type with the same parameters.
                if (!preserveParams)
                {
                    paramList.Clear();
                }

                return loadedType;
            }
            catch (Exception ex)
            {
                // Javascript-friendly exception
                throw new Exception(ex.Message);
            }
        }
        /// <summary>
        /// Call the object's Dispose method and sets the object to null;
        /// </summary>
        /// <param name="typeToUnload">Type implementing IDisposable to be destroyed</param>
        public void UnloadType(object typeToUnload)
        {
            try
            {
                if (typeToUnload != null && typeToUnload is IDisposable)
                {
                    (typeToUnload as IDisposable).Dispose();
                }

                typeToUnload = null;
            }
            catch { }
        }

        #endregion
    }
}
