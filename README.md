# Nokeya PhoneHelper

**Nokeya PhoneHelper** is a C# library that simulates classic multi-tap input on old mobile phones (Nokia-style). It converts sequences of keypad presses into text, supporting letters, symbols, spaces, and backspace.

---

## Features

- **Multi-tap letters:** Press the same key repeatedly to cycle letters (`2` → A, `22` → B, etc.).  
- **Symbols (`1`)**: Cycles `&'(`.  
- **Spaces (`0`)**: Inserts one or more spaces.  
- **Backspace (`*`)**: Deletes last character.  
- **Pause (space):** Separates consecutive presses of the same key.  
- **Send (`#`)**: Marks end of input.

---

## Keypad Mapping

| Key | Characters      |
|-----|----------------|
| 0   | Space          |
| 1   | & ' (          |
| 2-9 | Letters        |
| *   | Backspace      |
| #   | Send / End     |

---

## Usage

```csharp
using Nokeya.Helpers;

class Program
{
    static void Main()
    {
        string output = PhoneHelper.OldPhonePad("4433555 555666#");
        Console.WriteLine(output); // HELLO
    }
}
