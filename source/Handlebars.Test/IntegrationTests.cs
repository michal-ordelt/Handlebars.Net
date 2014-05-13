﻿using NUnit.Framework;
using System;

namespace Handlebars.Test
{
    [TestFixture]
    public class IntegrationTests
    {
        [Test]
        public void BasicPath()
        {
            var source = "Hello, {{name}}!";
            var template = Handlebars.Compile(source);
            var data = new {
                name = "Handlebars.Net"
            };
            var result = template(data);
            Assert.AreEqual("Hello, Handlebars.Net!", result);
        }

        [Test]
        public void BasicIfElse()
        {
            var source = "Hello, {{if basic_bool}}Bob{{else}}Sam{{/if}}!";
            var template = Handlebars.Compile(source);
            var trueData = new {
                basic_bool = true
            };
            var falseData = new {
                basic_bool = false
            };
            var resultTrue = template(trueData);
            var resultFalse = template(falseData);
            Assert.AreEqual("Hello, Bob!", resultTrue);
            Assert.AreEqual("Hello, Sam!", resultFalse);
        }

        [Test]
        public void BasicWith()
        {
            var source = "Hello,{{with person}} my good friend {{name}}{{/with}}!";
            var template = Handlebars.Compile(source);
            var data = new {
                person = new {
                    name = "Erik"
                }
            };
            var result = template(data);
            Assert.AreEqual("Hello, my good friend Erik!", result);
        }

        [Test]
        public void BasicIterator()
        {
            var source = "Hello,{{each people}}\n- {{name}}{{/each}}";
            var template = Handlebars.Compile(source);
            var data = new {
                people = new []{
                    new { 
                        name = "Erik"
                    },
                    new {
                        name = "Helen"
                    }
                }
            };
            var result = template(data);
            Assert.AreEqual("Hello,\n- Erik\n- Helen", result);
        }

        [Test]
        public void BasicIteratorWithIndex()
        {
            var source = "Hello,{{each people}}\n{{@index}}. {{name}}{{/each}}";
            var template = Handlebars.Compile(source);
            var data = new
            {
                people = new[]{
                    new { 
                        name = "Erik"
                    },
                    new {
                        name = "Helen"
                    }
                }
            };
            var result = template(data);
            Assert.AreEqual("Hello,\n0. Erik\n1. Helen", result);
        }

        [Test]
        public void BasicIteratorWithFirst()
        {
            var source = "Hello,{{each people}}\n{{@index}}. {{name}} ({{with @first}}{{name}} is first{{/with}}){{/each}}";
            var template = Handlebars.Compile(source);
            var data = new
            {
                people = new[]{
                    new { 
                        name = "Erik"
                    },
                    new {
                        name = "Helen"
                    }
                }
            };
            var result = template(data);
            Assert.AreEqual("Hello,\n0. Erik (Erik is first)\n1. Helen (Erik is first)", result);
        }

        [Test]
        public void BasicIteratorWithLast()
        {
            var source = "Hello,{{each people}}\n{{@index}}. {{name}} ({{with @last}}{{name}} is last{{/with}}){{/each}}";
            var template = Handlebars.Compile(source);
            var data = new
            {
                people = new[]{
                    new { 
                        name = "Erik"
                    },
                    new {
                        name = "Helen"
                    }
                }
            };
            var result = template(data);
            Assert.AreEqual("Hello,\n0. Erik (Helen is last)\n1. Helen (Helen is last)", result);
        }

        [Test]
        public void BasicIteratorEmpty()
        {
            var source = "Hello,{{each people}}\n- {{name}}{{else}} (no one listed){{/each}}";
            var template = Handlebars.Compile(source);
            var data = new
            {
                people = new object[] { }
            };
            var result = template(data);
            Assert.AreEqual("Hello, (no one listed)", result);
        }

        [Test]
        public void BasicEncoding()
        {
            var source = "Hello, {{name}}!";
            var template = Handlebars.Compile(source);
            var data = new
            {
                name = "<b>Bob</b>"
            };
            var result = template(data);
            Assert.AreEqual("Hello, &lt;b&gt;Bob&lt;/b&gt;!", result);
        }
    }
}
