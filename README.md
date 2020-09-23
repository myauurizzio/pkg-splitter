# pkg-splitter
Splits single pl/sql package file into two files. One file with package header and one file with package specification.

Using: PkgSplitter.exe PackageName.pkg

Result: PackageName.spc + PackageName.bdy

Program also adds a line with 'exit' command into the end of the result files.

Program uses "windows-1251" encoding for source and destination files (via System.Text.Encoding.GetEncoding). 
