using NUnit.Framework;
using Nokeya.Helpers;

namespace Nokeya.Tests
{
    public class PhoneHelperTests
    {
        [Test]
        public void Test_Simple()
        {
            Assert.That(PhoneHelper.OldPhonePad("33#"), Is.EqualTo("E"));
        }

        [Test]
        public void Test_Backspace()
        {
            Assert.That(PhoneHelper.OldPhonePad("227*#"), Is.EqualTo("B"));
        }

        [Test]
        public void Test_Hello()
        {
            Assert.That(PhoneHelper.OldPhonePad("4433555 555666#"), Is.EqualTo("HELLO"));
        }

        [Test]
        public void Test_Complex()
        {
            Assert.That(PhoneHelper.OldPhonePad("8 88777444666*664#"), Is.EqualTo("TURING"));
        }

        [Test]
        public void Test_Mixed_Symbols()
        {
            Assert.That(PhoneHelper.OldPhonePad("1#"), Is.EqualTo("&"));
        }

        [Test]
        public void Test_Wraparound()
        {
            Assert.That(PhoneHelper.OldPhonePad("7777#"), Is.EqualTo("S"));
        }

        [Test]
        public void Test_Pause()
        {
            Assert.That(PhoneHelper.OldPhonePad("44 444#"), Is.EqualTo("HI"));
        }

        [Test]
        public void Test_MultiplePauses()
        {
            Assert.That(PhoneHelper.OldPhonePad("44   444#"), Is.EqualTo("HI"));
        }

        [Test]
        public void Test_Space()
        {
            Assert.That(PhoneHelper.OldPhonePad("440444#"), Is.EqualTo("H I"));
        }

        [Test]
        public void Test_MultipleSpaces()
        {
            Assert.That(PhoneHelper.OldPhonePad("4400000444#"), Is.EqualTo("H     I"));
        }

        [Test]
        public void Test_EmptyBeforeHash()
        {
            Assert.That(PhoneHelper.OldPhonePad("#"), Is.EqualTo(""));
        }

        // Special Symbols Tests

        [Test]
        public void Test_OneKey_Single()
        {
            // 1 pressed once → &
            Assert.That(PhoneHelper.OldPhonePad("1#"), Is.EqualTo("&"));
        }

        [Test]
        public void Test_OneKey_Double()
        {
            // 1 pressed twice → '
            Assert.That(PhoneHelper.OldPhonePad("11#"), Is.EqualTo("'"));
        }

        [Test]
        public void Test_OneKey_Triple()
        {
            // 1 pressed three times → (
            Assert.That(PhoneHelper.OldPhonePad("111#"), Is.EqualTo("("));
        }

        [Test]
        public void Test_OneKey_Wraparound()
        {
            // 1 pressed four times → wraps back to &
            Assert.That(PhoneHelper.OldPhonePad("1111#"), Is.EqualTo("&"));
        }

        [Test]
        public void Test_OneKey_WithPause()
        {
            // 11 1 → ' followed by & (pause separates same key)
            Assert.That(PhoneHelper.OldPhonePad("11 1#"), Is.EqualTo("'&"));
        }

    }
}