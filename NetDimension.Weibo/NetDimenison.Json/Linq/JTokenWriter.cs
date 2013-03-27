﻿#region License

// Copyright (c) 2007 James Newton-King
//
// Permission is hereby granted, free of charge, to any person
// obtaining a copy of this software and associated documentation
// files (the "Software"), to deal in the Software without
// restriction, including without limitation the rights to use,
// copy, modify, merge, publish, distribute, sublicense, and/or sell
// copies of the Software, and to permit persons to whom the
// Software is furnished to do so, subject to the following
// conditions:
//
// The above copyright notice and this permission notice shall be
// included in all copies or substantial portions of the Software.
//
// THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND,
// EXPRESS OR IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES
// OF MERCHANTABILITY, FITNESS FOR A PARTICULAR PURPOSE AND
// NONINFRINGEMENT. IN NO EVENT SHALL THE AUTHORS OR COPYRIGHT
// HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER LIABILITY,
// WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING
// FROM, OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR
// OTHER DEALINGS IN THE SOFTWARE.

#endregion

using System;
using System.Globalization;
using NetDimension.Json.Utilities;

namespace NetDimension.Json.Linq
{
    /// <summary>
    ///     Represents a writer that provides a fast, non-cached, forward-only way of generating Json data.
    /// </summary>
    public class JTokenWriter : JsonWriter
    {
        private JContainer _token;
        private JContainer _parent;
        // used when writer is writing single value and the value has no containing parent
        private JValue _value;

        /// <summary>
        ///     Gets the token being writen.
        /// </summary>
        /// <value>The token being writen.</value>
        public JToken Token
        {
            get
            {
                if (_token != null)
                    return _token;

                return _value;
            }
        }

        /// <summary>
        ///     Initializes a new instance of the <see cref="JTokenWriter" /> class writing to the given <see cref="JContainer" />.
        /// </summary>
        /// <param name="container">The container being written to.</param>
        public JTokenWriter(JContainer container)
        {
            ValidationUtils.ArgumentNotNull(container, "container");

            _token = container;
            _parent = container;
        }

        /// <summary>
        ///     Initializes a new instance of the <see cref="JTokenWriter" /> class.
        /// </summary>
        public JTokenWriter()
        {
        }

        /// <summary>
        ///     Flushes whatever is in the buffer to the underlying streams and also flushes the underlying stream.
        /// </summary>
        public override void Flush()
        {
        }

        /// <summary>
        ///     Closes this stream and the underlying stream.
        /// </summary>
        public override void Close()
        {
            base.Close();
        }

        /// <summary>
        ///     Writes the beginning of a Json object.
        /// </summary>
        public override void WriteStartObject()
        {
            base.WriteStartObject();

            AddParent(new JObject());
        }

        private void AddParent(JContainer container)
        {
            if (_parent == null)
                _token = container;
            else
                _parent.AddAndSkipParentCheck(container);

            _parent = container;
        }

        private void RemoveParent()
        {
            _parent = _parent.Parent;

            if (_parent != null && _parent.Type == JTokenType.Property)
                _parent = _parent.Parent;
        }

        /// <summary>
        ///     Writes the beginning of a Json array.
        /// </summary>
        public override void WriteStartArray()
        {
            base.WriteStartArray();

            AddParent(new JArray());
        }

        /// <summary>
        ///     Writes the start of a constructor with the given name.
        /// </summary>
        /// <param name="name">The name of the constructor.</param>
        public override void WriteStartConstructor(string name)
        {
            base.WriteStartConstructor(name);

            AddParent(new JConstructor(name));
        }

        /// <summary>
        ///     Writes the end.
        /// </summary>
        /// <param name="token">The token.</param>
        protected override void WriteEnd(JsonToken token)
        {
            RemoveParent();
        }

        /// <summary>
        ///     Writes the property name of a name/value pair on a Json object.
        /// </summary>
        /// <param name="name">The name of the property.</param>
        public override void WritePropertyName(string name)
        {
            base.WritePropertyName(name);

            AddParent(new JProperty(name));
        }

        private void AddValue(object value, JsonToken token)
        {
            AddValue(new JValue(value), token);
        }

        internal void AddValue(JValue value, JsonToken token)
        {
            if (_parent != null)
            {
                _parent.Add(value);

                if (_parent.Type == JTokenType.Property)
                    _parent = _parent.Parent;
            }
            else
            {
                _value = value;
            }
        }

        #region WriteValue methods

        /// <summary>
        ///     Writes a null value.
        /// </summary>
        public override void WriteNull()
        {
            base.WriteNull();
            AddValue(null, JsonToken.Null);
        }

        /// <summary>
        ///     Writes an undefined value.
        /// </summary>
        public override void WriteUndefined()
        {
            base.WriteUndefined();
            AddValue(null, JsonToken.Undefined);
        }

        /// <summary>
        ///     Writes raw JSON.
        /// </summary>
        /// <param name="json">The raw JSON to write.</param>
        public override void WriteRaw(string json)
        {
            base.WriteRaw(json);
            AddValue(new JRaw(json), JsonToken.Raw);
        }

        /// <summary>
        ///     Writes out a comment <code>/*...*/</code> containing the specified text.
        /// </summary>
        /// <param name="text">Text to place inside the comment.</param>
        public override void WriteComment(string text)
        {
            base.WriteComment(text);
            AddValue(JValue.CreateComment(text), JsonToken.Comment);
        }

        /// <summary>
        ///     Writes a <see cref="string" /> value.
        /// </summary>
        /// <param name="value">
        ///     The <see cref="string" /> value to write.
        /// </param>
        public override void WriteValue(string value)
        {
            base.WriteValue(value);
            AddValue(value ?? string.Empty, JsonToken.String);
        }

        /// <summary>
        ///     Writes a <see cref="int" /> value.
        /// </summary>
        /// <param name="value">
        ///     The <see cref="int" /> value to write.
        /// </param>
        public override void WriteValue(int value)
        {
            base.WriteValue(value);
            AddValue(value, JsonToken.Integer);
        }

        /// <summary>
        ///     Writes a <see cref="uint" /> value.
        /// </summary>
        /// <param name="value">
        ///     The <see cref="uint" /> value to write.
        /// </param>
        [CLSCompliant(false)]
        public override void WriteValue(uint value)
        {
            base.WriteValue(value);
            AddValue(value, JsonToken.Integer);
        }

        /// <summary>
        ///     Writes a <see cref="long" /> value.
        /// </summary>
        /// <param name="value">
        ///     The <see cref="long" /> value to write.
        /// </param>
        public override void WriteValue(long value)
        {
            base.WriteValue(value);
            AddValue(value, JsonToken.Integer);
        }

        /// <summary>
        ///     Writes a <see cref="ulong" /> value.
        /// </summary>
        /// <param name="value">
        ///     The <see cref="ulong" /> value to write.
        /// </param>
        [CLSCompliant(false)]
        public override void WriteValue(ulong value)
        {
            base.WriteValue(value);
            AddValue(value, JsonToken.Integer);
        }

        /// <summary>
        ///     Writes a <see cref="float" /> value.
        /// </summary>
        /// <param name="value">
        ///     The <see cref="float" /> value to write.
        /// </param>
        public override void WriteValue(float value)
        {
            base.WriteValue(value);
            AddValue(value, JsonToken.Float);
        }

        /// <summary>
        ///     Writes a <see cref="double" /> value.
        /// </summary>
        /// <param name="value">
        ///     The <see cref="double" /> value to write.
        /// </param>
        public override void WriteValue(double value)
        {
            base.WriteValue(value);
            AddValue(value, JsonToken.Float);
        }

        /// <summary>
        ///     Writes a <see cref="bool" /> value.
        /// </summary>
        /// <param name="value">
        ///     The <see cref="bool" /> value to write.
        /// </param>
        public override void WriteValue(bool value)
        {
            base.WriteValue(value);
            AddValue(value, JsonToken.Boolean);
        }

        /// <summary>
        ///     Writes a <see cref="short" /> value.
        /// </summary>
        /// <param name="value">
        ///     The <see cref="short" /> value to write.
        /// </param>
        public override void WriteValue(short value)
        {
            base.WriteValue(value);
            AddValue(value, JsonToken.Integer);
        }

        /// <summary>
        ///     Writes a <see cref="ushort" /> value.
        /// </summary>
        /// <param name="value">
        ///     The <see cref="ushort" /> value to write.
        /// </param>
        [CLSCompliant(false)]
        public override void WriteValue(ushort value)
        {
            base.WriteValue(value);
            AddValue(value, JsonToken.Integer);
        }

        /// <summary>
        ///     Writes a <see cref="char" /> value.
        /// </summary>
        /// <param name="value">
        ///     The <see cref="char" /> value to write.
        /// </param>
        public override void WriteValue(char value)
        {
            base.WriteValue(value);
            string s = null;
#if !(NETFX_CORE || PORTABLE)
            s = value.ToString(CultureInfo.InvariantCulture);
#else
      s = value.ToString();
#endif
            AddValue(s, JsonToken.String);
        }

        /// <summary>
        ///     Writes a <see cref="byte" /> value.
        /// </summary>
        /// <param name="value">
        ///     The <see cref="byte" /> value to write.
        /// </param>
        public override void WriteValue(byte value)
        {
            base.WriteValue(value);
            AddValue(value, JsonToken.Integer);
        }

        /// <summary>
        ///     Writes a <see cref="sbyte" /> value.
        /// </summary>
        /// <param name="value">
        ///     The <see cref="sbyte" /> value to write.
        /// </param>
        [CLSCompliant(false)]
        public override void WriteValue(sbyte value)
        {
            base.WriteValue(value);
            AddValue(value, JsonToken.Integer);
        }

        /// <summary>
        ///     Writes a <see cref="decimal" /> value.
        /// </summary>
        /// <param name="value">
        ///     The <see cref="decimal" /> value to write.
        /// </param>
        public override void WriteValue(decimal value)
        {
            base.WriteValue(value);
            AddValue(value, JsonToken.Float);
        }

        /// <summary>
        ///     Writes a <see cref="DateTime" /> value.
        /// </summary>
        /// <param name="value">
        ///     The <see cref="DateTime" /> value to write.
        /// </param>
        public override void WriteValue(DateTime value)
        {
            base.WriteValue(value);
            value = JsonConvert.EnsureDateTime(value, DateTimeZoneHandling);
            AddValue(value, JsonToken.Date);
        }

#if !PocketPC
        /// <summary>
        ///     Writes a <see cref="DateTimeOffset" /> value.
        /// </summary>
        /// <param name="value">
        ///     The <see cref="DateTimeOffset" /> value to write.
        /// </param>
        public override void WriteValue(DateTimeOffset value)
        {
            base.WriteValue(value);
            AddValue(value, JsonToken.Date);
        }
#endif

        /// <summary>
        ///     Writes a <see cref="T:Byte[]" /> value.
        /// </summary>
        /// <param name="value">
        ///     The <see cref="T:Byte[]" /> value to write.
        /// </param>
        public override void WriteValue(byte[] value)
        {
            base.WriteValue(value);
            AddValue(value, JsonToken.Bytes);
        }

        /// <summary>
        ///     Writes a <see cref="TimeSpan" /> value.
        /// </summary>
        /// <param name="value">
        ///     The <see cref="TimeSpan" /> value to write.
        /// </param>
        public override void WriteValue(TimeSpan value)
        {
            base.WriteValue(value);
            AddValue(value, JsonToken.String);
        }

        /// <summary>
        ///     Writes a <see cref="Guid" /> value.
        /// </summary>
        /// <param name="value">
        ///     The <see cref="Guid" /> value to write.
        /// </param>
        public override void WriteValue(Guid value)
        {
            base.WriteValue(value);
            AddValue(value, JsonToken.String);
        }

        /// <summary>
        ///     Writes a <see cref="Uri" /> value.
        /// </summary>
        /// <param name="value">
        ///     The <see cref="Uri" /> value to write.
        /// </param>
        public override void WriteValue(Uri value)
        {
            base.WriteValue(value);
            AddValue(value, JsonToken.String);
        }

        #endregion
    }
}