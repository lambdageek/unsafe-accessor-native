using System.Runtime.InteropServices;
using System.Runtime.CompilerServices;

struct S {
        public int i;
        private static int _f;

        public S(int i, int f) { this.i = i; _f += f; }

        public void DumpIt() {
                Console.WriteLine ($"S: i = {i}, _f = {_f}");
        }

        private static void SetF(int f) { _f += f; }

}

public class Program
{
        const string Library = "libperturb";
        
        [DllImport(Library)]
        private static unsafe extern int perturb_it (delegate *unmanaged<S, int, void> accessor, int amt);

        [DllImport(Library)]
        private static unsafe extern S construct_it (delegate *unmanaged<int, int, S> ctor);

        [UnmanagedCallersOnly]
        [UnsafeAccessor(UnsafeAccessorKind.StaticMethod, Name="SetF")]
        private static extern void SetF (S @this, int f);

        [UnmanagedCallersOnly]
        [UnsafeAccessor(UnsafeAccessorKind.Constructor)]
        private static extern S ConstructS (int i, int f);
        
        [UnsafeAccessor(UnsafeAccessorKind.Constructor)]
        private static extern S ManagedConstructS (int i, int f);

        [MethodImpl(MethodImplOptions.NoInlining)]
        private static unsafe S IndirectMaker(delegate *<int, int, S> ctor)
        {
                return ctor (123, 456);
        }

        public static unsafe void Main()
        {
                Console.WriteLine ("Before construct_it:");
#if false
                // works
                S s = IndirectMaker (&ManagedConstructS);
#else
                // dies here
                S s = construct_it (&ConstructS);
#endif
                Console.WriteLine ("Before perturb_it:");
                s.DumpIt();
                perturb_it (&SetF, 1_000_000);
                Console.WriteLine ("After:");
                s.DumpIt();
        }
}
