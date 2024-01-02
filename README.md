# RhinoGator

## About
This is a logic circuit simulator.

## Goals
- Implementing different logic circuits in source code, based on some simple basic logic elements, only (no cheating!). The final goal is implementing the whole SAP-1 (Simple As Possible computer) this way, or at least parts of it.
- Understanding more complicated logic circuits by coding the simulation and making sure that they work before building them with hardware.
- Be compatible with Linux and Windows operating systems.
- Testing the productivity of using Visual Studio Code with Linux for development of a C# .NET console application (instead of using Visual Studio with Windows).
- Maybe add support for text-based input files (e.g. JSON) describing the logic circuits to be simulated instead of doing that statically in source code.

## How to use
- Select one of the example classes (or create your own) and use it in `Program.cs`.
- Run the application with .NET at the console/terminal.
- The UI depends on the example selected, the controls on the example and the game loop, see `GameLoop.cs`.

## Sources

The page numbers mentioned in code and the implemented logic elements (basic and so-called assembled elements) are from the book Digital Computer Electronics by Albert Paul Malvino and Jerald A. Brown.
