namespace Problem01_TextEditor
{
    using System.Collections.Generic;
    using System.Linq;

    /// <summary>
    /// login {username} – the given user can start executing commands
    /// logout {username} – the given user can no longer edit his string
    /// {username} {command} {parameters} – this is the pattern we will be using for the following user commands:
    ///     insert { index } {string} – inserts the given string in the given position
    ///     prepend {string} – inserts the given string in the beginning of the string
    ///     substring {start index} {length} – replaces the user string with a substring from it
    ///     delete { start index } {length} – removes part of the user string
    ///     clear – deletes the user string
    ///     length – returns the length of the user string
    ///     print – returns the user string
    ///     undo – reverts the last operations on the user string. Can be used multiple times
    /// users – returns all users currently logged in
    /// users {prefix} – returns all users starting with the given prefix
    /// end – stops the program
    /// </summary>
    public class TextEditor : ITextEditor
    {
        private readonly Dictionary<string, int> users;
        private readonly Trie<Stack<string>> userStack;
        private Trie<BigList<char>> userString;

        public TextEditor()
        {
            this.userString = new Trie<BigList<char>>();
            this.userStack = new Trie<Stack<string>>();
            this.users = new Dictionary<string, int>();
        }

        public void Login(string username)
        {
            this.userString.Insert(username, new BigList<char>());
            this.userStack.Insert(username, new Stack<string>());
            if (!this.users.ContainsKey(username))
            {
                this.users.Add(username, this.users.Count + 1);
            }
        }

        public void Logout(string username)
        {
            this.userString.Delete(username);
            this.userStack.Delete(username);
            if (this.users.ContainsKey(username))
            {
                this.users.Remove(username);
            }
        }

        public void Prepend(string username, string str)
        {
            if (!this.userString.Contains(username))
            {
                return;
            }

            this.userStack.GetValue(username).Push(this.Print(username));
            this.userString.GetValue(username).AddRangeToFront(str);
        }

        public void Insert(string username, int index, string str)
        {
            if (!this.userString.Contains(username))
            {
                return;
            }

            this.userStack.GetValue(username).Push(this.GetJoinnedCharacters(this.userString.GetValue(username)));
            this.userString.GetValue(username).InsertRange(index, str);
        }

        public void Substring(string username, int startIndex, int length)
        {
            if (!this.userString.Contains(username))
            {
                return;
            }

            var currentUserString = this.userString.GetValue(username);
            this.userStack.GetValue(username)
                .Push(this.GetJoinnedCharacters(currentUserString));
            var newUserString = new BigList<char>(
                this.GetJoinnedCharacters(currentUserString)
                    .Skip(startIndex)
                    .Take(length));
            this.userString = new Trie<BigList<char>>();
            this.userString.Insert(username, newUserString);
        }

        public void Delete(string username, int startIndex, int length)
        {
            if (!this.userString.Contains(username))
            {
                return;
            }

            this.userString.GetValue(username).RemoveRange(startIndex, length);
        }

        public void Clear(string username)
        {
            if (!this.userString.Contains(username))
            {
                return;
            }

            this.userStack.GetValue(username)
                .Push(this.GetJoinnedCharacters(this.userString.GetValue(username)));
            this.userString.Insert(username, new BigList<char>());
        }

        public int Length(string username)
        {
            return this.Print(username).Length;
        }

        public string Print(string username)
        {
            if (!this.userString.Contains(username))
            {
                return string.Empty;
            }

            return this.GetJoinnedCharacters(this.userString.GetValue(username));
        }

        public void Undo(string username)
        {
            if (this.userString.Contains(username))
            {
                return;
            }

            if (this.userStack.GetValue(username).Count == 0)
            {
                return;
            }

            var last = this.userStack.GetValue(username).Pop();
            this.userStack.GetValue(username).Push(this.GetJoinnedCharacters(this.userString.GetValue(username)));
            this.userString.Insert(username, new BigList<char>(last));
        }

        public IEnumerable<string> Users(string prefix = "")
        {
            if (prefix != "")
            {
                return this.users
                    .Where(u => u.Key.StartsWith(prefix))
                    .OrderBy(u => u.Value)
                    .Select(u => u.Key);
            }

            return this.users.OrderBy(u => u.Value).Select(u => u.Key);
        }

        private string GetJoinnedCharacters(IEnumerable<char> characters)
        {
            return string.Join(string.Empty, characters);
        }
    }
}