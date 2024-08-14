using System;
using System.Runtime.InteropServices;

using Lua;

namespace luaTest
{
    class Program
    {
        static LuaState theLuaVm;

        static void Main(string[] args)
        {
            theLuaVm = new LuaState();

            bool quit = false;
            System.Console.WriteLine("Lua Test Interpretor");
            while (!quit)
            {
                System.Console.Write("> ");
                string line = System.Console.ReadLine();
                if (line == "quit" || line == "exit")
                {
                    quit = true;
                    continue;
                }
                else if(line == "stackdump")
                {
                    theLuaVm.stackDump();
                }

                else
                {
                    var result = theLuaVm.doString(line);
                    if (LuaThreadStatus.LUA_ERRSYNTAX == (LuaThreadStatus)result)
                    {
                        
                        IntPtr errorPtr = LuaDLL.lua_tostring(theLuaVm.statePtr, -1);
                        string? errorMessage = Marshal.PtrToStringAnsi(errorPtr);
                        System.Console.WriteLine(errorMessage);
                    }
                }


            }

            theLuaVm.close();
        }
    }
}
