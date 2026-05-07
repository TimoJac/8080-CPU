
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _8080_CPU
{
    internal class Program
    {

        unsafe public static void Main(string[] args)
        {
            var p = new Program();

            p.ReadFile();

            
        }

        public void ReadFile()
        {
            FileStream fs = new FileStream(@"C:\Dev\Emulators\8080-CPU\ROM-SpaceInvaders\invaders", FileMode.Open);
            if (fs == null)
            {
                Console.WriteLine(String.Format("Error: Couldn't open file"));
                Environment.Exit(1);
            }


            fs.Seek(0, SeekOrigin.End);
            int fSize = (int)fs.Position;
            fs.Seek(0, SeekOrigin.Begin);

            byte[] buffer = new byte[fSize];

            fs.Read(buffer, 0, fSize);
            fs.Close();
            
            int pc = 0;

            while (pc < fSize)
            {
                pc += Disassemble8080Op(buffer, pc);
            }
        }

        /// <summary>
        /// Disassembler - Translates a stream of hex numbers back into assembly language source.
        /// </summary>
        /// <param name="codeBuffer">Valid pointer to 8080 assembly code</param>
        /// <param name="pc">Current offset into the code</param>
        /// <returns>Number of bytes of the op (</returns>
        public int Disassemble8080Op(byte[] codeBuffer, int pc)
        {
            List<byte> code = new List<byte>(codeBuffer);
            code.RemoveRange(0, pc);
            int opBytes = 1;
            Console.Write(String.Format("#{0:X4} ", pc));

            switch (code[0])
            {
                case 0x00: Console.WriteLine("NOP"); break;
                case 0x01: Console.WriteLine(String.Format("LXI    B,#${0:X2}{1:X2}", code[2], code[1])); opBytes = 3; break;
                case 0x02: Console.WriteLine(String.Format("STAX   B")); break;
                case 0x03: Console.WriteLine(String.Format("INX    B")); break;
                case 0x04: Console.WriteLine(String.Format("INR    B")); break;
                case 0x05: Console.WriteLine(String.Format("DCR    B")); break;
                case 0x06: Console.WriteLine(String.Format("MVI    B,#${0:X2}", code[1])); opBytes = 2; break;
                case 0x07: Console.WriteLine(String.Format("RLC     ")); break;
                case 0x08: Console.WriteLine(String.Format("-")); break;
                case 0x09: Console.WriteLine(String.Format("DAD    B")); break;
                case 0x0a: Console.WriteLine(String.Format("LDAX   B")); break;
                case 0x0b: Console.WriteLine(String.Format("DCX    B")); break;
                case 0x0c: Console.WriteLine(String.Format("INR    C")); break;
                case 0x0d: Console.WriteLine(String.Format("DCR    C")); break;
                case 0x0e: Console.WriteLine(String.Format("MVI    C,#${0:X2}", code[1])); opBytes = 2; break;
                case 0x0f: Console.WriteLine(String.Format("RRC     ")); break;
                                        
                case 0x10: Console.WriteLine(String.Format("-")); break;
                case 0x11: Console.WriteLine(String.Format("LXI    D,#${0:X2}{1:X2}", code[2], code[1])); opBytes = 3; break;
                case 0x12: Console.WriteLine(String.Format("STAX   D")); break;
                case 0x13: Console.WriteLine(String.Format("INX    D")); break;
                case 0x14: Console.WriteLine(String.Format("INR    D")); break;
                case 0x15: Console.WriteLine(String.Format("DCR    D")); break;
                case 0x16: Console.WriteLine(String.Format("MVI    D,#${0:X2}", code[1])); opBytes = 2; break;
                case 0x17: Console.WriteLine(String.Format("RAL     ")); break;
                case 0x18: Console.WriteLine(String.Format("-")); break;
                case 0x19: Console.WriteLine(String.Format("DAD    D")); break;
                case 0x1a: Console.WriteLine(String.Format("LDAX   D")); break;
                case 0x1b: Console.WriteLine(String.Format("DCX    D")); break;
                case 0x1c: Console.WriteLine(String.Format("INR    E")); break;
                case 0x1d: Console.WriteLine(String.Format("DCR    E")); break;
                case 0x1e: Console.WriteLine(String.Format("MVI    E,#${0:X2}", code[1])); opBytes = 2; break;
                case 0x1f: Console.WriteLine(String.Format("RAR     ")); break;
                                        
                case 0x20: Console.WriteLine(String.Format("-")); break;
                case 0x21: Console.WriteLine(String.Format("LXI    H,#${0:X2}{1:X2}", code[2], code[1])); opBytes = 3; break;
                case 0x22: Console.WriteLine(String.Format("SHLD   ${0:X2}{1:X2}", code[2], code[1])); opBytes = 3; break;
                case 0x23: Console.WriteLine(String.Format("INX    H")); break;
                case 0x24: Console.WriteLine(String.Format("INR    H")); break;
                case 0x25: Console.WriteLine(String.Format("DCR    H")); break;
                case 0x26: Console.WriteLine(String.Format("MVI    H,#${0:X2}", code[1])); opBytes = 2; break;
                case 0x27: Console.WriteLine(String.Format("DAA     ")); break;
                case 0x28: Console.WriteLine(String.Format("-")); break;
                case 0x29: Console.WriteLine(String.Format("DAD    H")); break;
                case 0x2a: Console.WriteLine(String.Format("LHLD   ${0:X2}{1:X2}", code[2], code[1])); opBytes = 3; break;
                case 0x2b: Console.WriteLine(String.Format("DCX    H")); break;
                case 0x2c: Console.WriteLine(String.Format("INR    L")); break;
                case 0x2d: Console.WriteLine(String.Format("DCR    L")); break;
                case 0x2e: Console.WriteLine(String.Format("MVI    L,#${0:X2}", code[1])); opBytes = 2; break;
                case 0x2f: Console.WriteLine(String.Format("CMA     ")); break;
                                        
                case 0x30: Console.WriteLine(String.Format("-")); break;
                case 0x31: Console.WriteLine(String.Format("LXI    SP,#${0:X2}{1:X2}", code[2], code[1])); opBytes = 3; break;
                case 0x32: Console.WriteLine(String.Format("STA    ${0:X2}{1:X2}", code[2], code[1])); opBytes = 3; break;
                case 0x33: Console.WriteLine(String.Format("INX    SP")); break;
                case 0x34: Console.WriteLine(String.Format("INR    M")); break;
                case 0x35: Console.WriteLine(String.Format("DCR    M")); break;
                case 0x36: Console.WriteLine(String.Format("MVI    M,#${0:X2}", code[1])); opBytes = 2; break;
                case 0x37: Console.WriteLine(String.Format("STC     ")); break;
                case 0x38: Console.WriteLine(String.Format("-")); break;
                case 0x39: Console.WriteLine(String.Format("DAD    SP")); break;
                case 0x3a: Console.WriteLine(String.Format("LDA    ${0:X2}", code[1])); opBytes = 2; break;
                case 0x3b: Console.WriteLine(String.Format("DCX    SP")); break;
                case 0x3c: Console.WriteLine(String.Format("INR    A")); break;
                case 0x3d: Console.WriteLine(String.Format("DCR    A")); break;
                case 0x3e: Console.WriteLine(String.Format("MVI    A,#${0:X2}", code[1])); opBytes = 2; break;
                case 0x3f: Console.WriteLine(String.Format("CMC     ")); break;
                                        
                case 0x40: Console.WriteLine(String.Format("MOV    B,B")); break;
                case 0x41: Console.WriteLine(String.Format("MOV    B,C")); break;
                case 0x42: Console.WriteLine(String.Format("MOV    B,D")); break;
                case 0x43: Console.WriteLine(String.Format("MOV    B,E")); break;
                case 0x44: Console.WriteLine(String.Format("MOV    B,H")); break;
                case 0x45: Console.WriteLine(String.Format("MOV    B,L")); break;
                case 0x46: Console.WriteLine(String.Format("MOV    B,M")); break;
                case 0x47: Console.WriteLine(String.Format("MOV    B,A")); break;
                case 0x48: Console.WriteLine(String.Format("MOV    C,B")); break;
                case 0x49: Console.WriteLine(String.Format("MOV    C,C")); break;
                case 0x4a: Console.WriteLine(String.Format("MOV    C,D")); break;
                case 0x4b: Console.WriteLine(String.Format("MOV    C,E")); break;
                case 0x4c: Console.WriteLine(String.Format("MOV    C,H")); break;
                case 0x4d: Console.WriteLine(String.Format("MOV    C,L")); break;
                case 0x4e: Console.WriteLine(String.Format("MOV    C,M")); break;
                case 0x4f: Console.WriteLine(String.Format("MOV    C,A")); break;
                                        
                case 0x50: Console.WriteLine(String.Format("MOV    D,B")); break;
                case 0x51: Console.WriteLine(String.Format("MOV    D,C")); break;
                case 0x52: Console.WriteLine(String.Format("MOV    D,D")); break;
                case 0x53: Console.WriteLine(String.Format("MOV    D,E")); break;
                case 0x54: Console.WriteLine(String.Format("MOV    D,H")); break;
                case 0x55: Console.WriteLine(String.Format("MOV    D,L")); break;
                case 0x56: Console.WriteLine(String.Format("MOV    D,M")); break;
                case 0x57: Console.WriteLine(String.Format("MOV    D,A")); break;
                case 0x58: Console.WriteLine(String.Format("MOV    E,B")); break;
                case 0x59: Console.WriteLine(String.Format("MOV    E,C")); break;
                case 0x5a: Console.WriteLine(String.Format("MOV    E,D")); break;
                case 0x5b: Console.WriteLine(String.Format("MOV    E,E")); break;
                case 0x5c: Console.WriteLine(String.Format("MOV    E,H")); break;
                case 0x5d: Console.WriteLine(String.Format("MOV    E,L")); break;
                case 0x5e: Console.WriteLine(String.Format("MOV    E,M")); break;
                case 0x5f: Console.WriteLine(String.Format("MOV    E,A")); break;
                                        
                case 0x60: Console.WriteLine(String.Format("MOV    H,B")); break;
                case 0x61: Console.WriteLine(String.Format("MOV    H,C")); break;
                case 0x62: Console.WriteLine(String.Format("MOV    H,D")); break;
                case 0x63: Console.WriteLine(String.Format("MOV    H,E")); break;
                case 0x64: Console.WriteLine(String.Format("MOV    H,H")); break;
                case 0x65: Console.WriteLine(String.Format("MOV    H,L")); break;
                case 0x66: Console.WriteLine(String.Format("MOV    H,M")); break;
                case 0x67: Console.WriteLine(String.Format("MOV    H,A")); break;
                case 0x68: Console.WriteLine(String.Format("MOV    L,B")); break;
                case 0x69: Console.WriteLine(String.Format("MOV    L,C")); break;
                case 0x6a: Console.WriteLine(String.Format("MOV    L,D")); break;
                case 0x6b: Console.WriteLine(String.Format("MOV    L,E")); break;
                case 0x6c: Console.WriteLine(String.Format("MOV    L,H")); break;
                case 0x6d: Console.WriteLine(String.Format("MOV    L,L")); break;
                case 0x6e: Console.WriteLine(String.Format("MOV    L,M")); break;
                case 0x6f: Console.WriteLine(String.Format("MOV    L,A")); break;
                                        
                case 0x70: Console.WriteLine(String.Format("MOV    M,B")); break;
                case 0x71: Console.WriteLine(String.Format("MOV    M,C")); break;
                case 0x72: Console.WriteLine(String.Format("MOV    M,D")); break;
                case 0x73: Console.WriteLine(String.Format("MOV    M,E")); break;
                case 0x74: Console.WriteLine(String.Format("MOV    M,H")); break;
                case 0x75: Console.WriteLine(String.Format("MOV    M,L")); break;
                case 0x76: Console.WriteLine(String.Format("HLT     ")); break;
                case 0x77: Console.WriteLine(String.Format("MOV    M,A")); break;
                case 0x78: Console.WriteLine(String.Format("MOV    A,B")); break;
                case 0x79: Console.WriteLine(String.Format("MOV    A,C")); break;
                case 0x7a: Console.WriteLine(String.Format("MOV    A,D")); break;
                case 0x7b: Console.WriteLine(String.Format("MOV    A,E")); break;
                case 0x7c: Console.WriteLine(String.Format("MOV    A,H")); break;
                case 0x7d: Console.WriteLine(String.Format("MOV    A,L")); break;
                case 0x7e: Console.WriteLine(String.Format("MOV    A,M")); break;
                case 0x7f: Console.WriteLine(String.Format("MOV    A,A")); break;
                                        
                case 0x80: Console.WriteLine(String.Format("ADD    B")); break;
                case 0x81: Console.WriteLine(String.Format("ADD    C")); break;
                case 0x82: Console.WriteLine(String.Format("ADD    D")); break;
                case 0x83: Console.WriteLine(String.Format("ADD    E")); break;
                case 0x84: Console.WriteLine(String.Format("ADD    H")); break;
                case 0x85: Console.WriteLine(String.Format("ADD    L")); break;
                case 0x86: Console.WriteLine(String.Format("ADD    M")); break;
                case 0x87: Console.WriteLine(String.Format("ADD    A")); break;
                case 0x88: Console.WriteLine(String.Format("ADC    B")); break;
                case 0x89: Console.WriteLine(String.Format("ADC    C")); break;
                case 0x8a: Console.WriteLine(String.Format("ADC    D")); break;
                case 0x8b: Console.WriteLine(String.Format("ADC    E")); break;
                case 0x8c: Console.WriteLine(String.Format("ADC    H")); break;
                case 0x8d: Console.WriteLine(String.Format("ADC    L")); break;
                case 0x8e: Console.WriteLine(String.Format("ADC    M")); break;
                case 0x8f: Console.WriteLine(String.Format("ADC    A")); break;
                                        
                case 0x90: Console.WriteLine(String.Format("SUB    B")); break;
                case 0x91: Console.WriteLine(String.Format("SUB    C")); break;
                case 0x92: Console.WriteLine(String.Format("SUB    D")); break;
                case 0x93: Console.WriteLine(String.Format("SUB    E")); break;
                case 0x94: Console.WriteLine(String.Format("SUB    H")); break;
                case 0x95: Console.WriteLine(String.Format("SUB    L")); break;
                case 0x96: Console.WriteLine(String.Format("SUB    M")); break;
                case 0x97: Console.WriteLine(String.Format("SUB    A")); break;
                case 0x98: Console.WriteLine(String.Format("SBB    B")); break;
                case 0x99: Console.WriteLine(String.Format("SBB    C")); break;
                case 0x9a: Console.WriteLine(String.Format("SBB    D")); break;
                case 0x9b: Console.WriteLine(String.Format("SBB    E")); break;
                case 0x9c: Console.WriteLine(String.Format("SBB    H")); break;
                case 0x9d: Console.WriteLine(String.Format("SBB    L")); break;
                case 0x9e: Console.WriteLine(String.Format("SBB    M")); break;
                case 0x9f: Console.WriteLine(String.Format("SBB    A")); break;
                                        
                case 0xa0: Console.WriteLine(String.Format("ANA    B")); break;
                case 0xa1: Console.WriteLine(String.Format("ANA    C")); break;
                case 0xa2: Console.WriteLine(String.Format("ANA    D")); break;
                case 0xa3: Console.WriteLine(String.Format("ANA    E")); break;
                case 0xa4: Console.WriteLine(String.Format("ANA    H")); break;
                case 0xa5: Console.WriteLine(String.Format("ANA    L")); break;
                case 0xa6: Console.WriteLine(String.Format("ANA    M")); break;
                case 0xa7: Console.WriteLine(String.Format("ANA    A")); break;
                case 0xa8: Console.WriteLine(String.Format("XRA    B")); break;
                case 0xa9: Console.WriteLine(String.Format("XRA    C")); break;
                case 0xaa: Console.WriteLine(String.Format("XRA    D")); break;
                case 0xab: Console.WriteLine(String.Format("XRA    E")); break;
                case 0xac: Console.WriteLine(String.Format("XRA    H")); break;
                case 0xad: Console.WriteLine(String.Format("XRA    L")); break;
                case 0xae: Console.WriteLine(String.Format("XRA    M")); break;
                case 0xaf: Console.WriteLine(String.Format("XRA    A")); break;
                                        
                case 0xb0: Console.WriteLine(String.Format("ORA    B")); break;
                case 0xb1: Console.WriteLine(String.Format("ORA    C")); break;
                case 0xb2: Console.WriteLine(String.Format("ORA    D")); break;
                case 0xb3: Console.WriteLine(String.Format("ORA    E")); break;
                case 0xb4: Console.WriteLine(String.Format("ORA    H")); break;
                case 0xb5: Console.WriteLine(String.Format("ORA    L")); break;
                case 0xb6: Console.WriteLine(String.Format("ORA    M")); break;
                case 0xb7: Console.WriteLine(String.Format("ORA    A")); break;
                case 0xb8: Console.WriteLine(String.Format("CMP    B")); break;
                case 0xb9: Console.WriteLine(String.Format("CMP    C")); break;
                case 0xba: Console.WriteLine(String.Format("CMP    D")); break;
                case 0xbb: Console.WriteLine(String.Format("CMP    E")); break;
                case 0xbc: Console.WriteLine(String.Format("CMP    H")); break;
                case 0xbd: Console.WriteLine(String.Format("CMP    L")); break;
                case 0xbe: Console.WriteLine(String.Format("CMP    M")); break;
                case 0xbf: Console.WriteLine(String.Format("CMP    A")); break;
                                        
                case 0xc0: Console.WriteLine(String.Format("RNZ     ")); break;
                case 0xc1: Console.WriteLine(String.Format("POP    B")); break;
                case 0xc2: Console.WriteLine(String.Format("JNZ    ${0:X2}{1:X2}", code[2], code[1])); opBytes = 3; break;
                case 0xc3: Console.WriteLine(String.Format("JMP    ${0:X2}{1:X2}", code[2], code[1])); opBytes = 3; break;
                case 0xc4: Console.WriteLine(String.Format("CNZ    ${0:X2}{1:X2}", code[2], code[1])); opBytes = 3; break;
                case 0xc5: Console.WriteLine(String.Format("PUSH   B")); break;
                case 0xc6: Console.WriteLine(String.Format("ADI    #${0:X2}", code[1])); opBytes = 2; break;
                case 0xc7: Console.WriteLine(String.Format("RST    0")); break;
                case 0xc8: Console.WriteLine(String.Format("RZ      ")); break;
                case 0xc9: Console.WriteLine(String.Format("RET     ")); break;
                case 0xca: Console.WriteLine(String.Format("JZ     ${0:X2}{1:X2}", code[2], code[1])); opBytes = 3; break;
                case 0xcb: Console.WriteLine(String.Format("-")); break;
                case 0xcc: Console.WriteLine(String.Format("CZ     ${0:X2}{1:X2}", code[2], code[1])); opBytes = 3; break;
                case 0xcd: Console.WriteLine(String.Format("CALL   ${0:X2}{1:X2}", code[2], code[1])); opBytes = 3; break;
                case 0xce: Console.WriteLine(String.Format("ACI    #${0:X2}", code[1])); opBytes = 2; break;
                case 0xcf: Console.WriteLine(String.Format("RST    1")); break;
                                        
                case 0xd0: Console.WriteLine(String.Format("RNC     ")); break;
                case 0xd1: Console.WriteLine(String.Format("POP    D")); break;
                case 0xd2: Console.WriteLine(String.Format("JNC    ${0:X2}{1:X2}", code[2], code[1])); opBytes = 3; break;
                case 0xd3: Console.WriteLine(String.Format("OUT    #${0:X2}", code[1])); opBytes = 2; break;
                case 0xd4: Console.WriteLine(String.Format("CNC    ${0:X2}{1:X2}", code[2], code[1])); opBytes = 3; break;
                case 0xd5: Console.WriteLine(String.Format("PUSH   D")); break;
                case 0xd6: Console.WriteLine(String.Format("SUI    #${0:X2}", code[1])); opBytes = 2; break;
                case 0xd7: Console.WriteLine(String.Format("RST    2")); break;
                case 0xd8: Console.WriteLine(String.Format("RC      ")); break;
                case 0xd9: Console.WriteLine(String.Format("-")); break;
                case 0xda: Console.WriteLine(String.Format("JC     ${0:X2}{1:X2}", code[2], code[1])); opBytes = 3; break;
                case 0xdb: Console.WriteLine(String.Format("IN     #${0:X2}", code[1])); opBytes = 2; break;
                case 0xdc: Console.WriteLine(String.Format("CC     ${0:X2}{1:X2}", code[2], code[1])); opBytes = 3; break;
                case 0xdd: Console.WriteLine(String.Format("-")); break;
                case 0xde: Console.WriteLine(String.Format("DBI    #${0:X2}", code[1])); opBytes = 2; break;
                case 0xdf: Console.WriteLine(String.Format("RST    3")); break;
                                        
                case 0xe0: Console.WriteLine(String.Format("RPO     ")); break;
                case 0xe1: Console.WriteLine(String.Format("POP    H")); break;
                case 0xe2: Console.WriteLine(String.Format("JPO    ${0:X2}{1:X2}", code[2], code[1])); opBytes = 3; break;
                case 0xe3: Console.WriteLine(String.Format("XTHL    ")); break;
                case 0xe4: Console.WriteLine(String.Format("CPO    ${0:X2}{1:X2}", code[2], code[1])); opBytes = 3; break;
                case 0xe5: Console.WriteLine(String.Format("PUSH   H")); break;
                case 0xe6: Console.WriteLine(String.Format("ANI    #${0:X2}", code[1])); opBytes = 2; break;
                case 0xe7: Console.WriteLine(String.Format("RST    4")); break;
                case 0xe8: Console.WriteLine(String.Format("RPE     ")); break;
                case 0xe9: Console.WriteLine(String.Format("PCHL    ")); break;
                case 0xea: Console.WriteLine(String.Format("JPE    ${0:X2}{1:X2}", code[2], code[1])); opBytes = 3; break;
                case 0xeb: Console.WriteLine(String.Format("XCHG    ")); break;
                case 0xec: Console.WriteLine(String.Format("CPE    ${0:X2}{1:X2}", code[2], code[1])); opBytes = 3; break;
                case 0xed: Console.WriteLine(String.Format("-")); break;
                case 0xee: Console.WriteLine(String.Format("XRI    #${0:X2}", code[1])); opBytes = 2; break;
                case 0xef: Console.WriteLine(String.Format("RST    5")); break;
                                        
                case 0xf0: Console.WriteLine(String.Format("RP      ")); break;
                case 0xf1: Console.WriteLine(String.Format("POP    PSW")); break;
                case 0xf2: Console.WriteLine(String.Format("JP     ${0:X2}{1:X2}", code[2], code[1])); opBytes = 3; break;
                case 0xf3: Console.WriteLine(String.Format("DI      ")); break;
                case 0xf4: Console.WriteLine(String.Format("CP     ${0:X2}{1:X2}", code[2], code[1])); opBytes = 3; break;
                case 0xf5: Console.WriteLine(String.Format("PUSH   PSW")); break;
                case 0xf6: Console.WriteLine(String.Format("ORI    #${0:X2}", code[1])); opBytes = 2; break;
                case 0xf7: Console.WriteLine(String.Format("RST    6")); break;
                case 0xf8: Console.WriteLine(String.Format("RM      ")); break;
                case 0xf9: Console.WriteLine(String.Format("SPHL    ")); break;
                case 0xfa: Console.WriteLine(String.Format("JM     ${0:X2}{1:X2}", code[2], code[1])); opBytes = 3; break;
                case 0xfb: Console.WriteLine(String.Format("EI      ")); break;
                case 0xfc: Console.WriteLine(String.Format("CM     ${0:X4}{1:X2}", code[2], code[1])); opBytes = 3; break;
                case 0xfd: Console.WriteLine(String.Format("-")); break;
                case 0xfe: Console.WriteLine(String.Format("CPI    #${0:X2}", code[1])); opBytes = 2; break;
                case 0xff: Console.WriteLine(String.Format("RST    7")); break;
            }


            return opBytes;
        }
    }
}
