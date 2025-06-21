using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Forms;
using Microsoft.Extensions.DependencyInjection;
using Radzen.Blazor;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Radzen
{
    /// <summary>
    /// Class with IServiceCollection extensions methods.
    /// </summary>
    public static class ServiceCollectionExtensions
    {
        /// <summary>
        /// Add Radzen Blazor components services
        /// </summary>
        /// <param name="services">Service collection</param>
        public static IServiceCollection AddRadzenComponents(this IServiceCollection services)
        {
            services.AddScoped<DialogService>();
            services.AddScoped<NotificationService>();
            services.AddScoped<ThemeService>();

            return services;
        }
    }

    /// <summary>
    /// Colors.
    /// </summary>
    public static class Colors
    {
        /// <summary>
        /// Primary.
        /// </summary>
        public const string Primary = "var(--rz-primary)";

        /// <summary>
        /// Primary lighter.
        /// </summary>
        public const string PrimaryLighter = "var(--rz-primary-lighter)";

        /// <summary>
        /// Primary light.
        /// </summary>
        public const string PrimaryLight = "var(--rz-primary-light)";

        /// <summary>
        /// Primary dark.
        /// </summary>
        public const string PrimaryDark = "var(--rz-primary-dark)";

        /// <summary>
        /// Primary darker.
        /// </summary>
        public const string PrimaryDarker = "var(--rz-primary-darker)";

        /// <summary>
        /// Secondary.
        /// </summary>
        public const string Secondary = "var(--rz-secondary)";

        /// <summary>
        /// Secondary lighter.
        /// </summary>
        public const string SecondaryLighter = "var(--rz-secondary-lighter)";

        /// <summary>
        /// Secondary light.
        /// </summary>
        public const string SecondaryLight = "var(--rz-secondary-light)";

        /// <summary>
        /// Secondary dark.
        /// </summary>
        public const string SecondaryDark = "var(--rz-secondary-dark)";

        /// <summary>
        /// Secondary darker.
        /// </summary>
        public const string SecondaryDarker = "var(--rz-secondary-darker)";

        /// <summary>
        /// Info.
        /// </summary>
        public const string Info = "var(--rz-info)";

        /// <summary>
        /// Info lighter.
        /// </summary>
        public const string InfoLighter = "var(--rz-info-lighter)";

        /// <summary>
        /// Info light.
        /// </summary>
        public const string InfoLight = "var(--rz-info-light)";

        /// <summary>
        /// Info dark.
        /// </summary>
        public const string InfoDark = "var(--rz-info-dark)";

        /// <summary>
        /// Info darker.
        /// </summary>
        public const string InfoDarker = "var(--rz-info-darker)";

        /// <summary>
        /// Success.
        /// </summary>
        public const string Success = "var(--rz-success)";

        /// <summary>
        /// Success lighter.
        /// </summary>
        public const string SuccessLighter = "var(--rz-success-lighter)";

        /// <summary>
        /// Success light.
        /// </summary>
        public const string SuccessLight = "var(--rz-success-light)";

        /// <summary>
        /// Success dark.
        /// </summary>
        public const string SuccessDark = "var(--rz-success-dark)";

        /// <summary>
        /// Success darker.
        /// </summary>
        public const string SuccessDarker = "var(--rz-success-darker)";

        /// <summary>
        /// Warning.
        /// </summary>
        public const string Warning = "var(--rz-warning)";

        /// <summary>
        /// Warning lighter.
        /// </summary>
        public const string WarningLighter = "var(--rz-warning-lighter)";

        /// <summary>
        /// Warning light.
        /// </summary>
        public const string WarningLight = "var(--rz-warning-light)";

        /// <summary>
        /// Warning dark.
        /// </summary>
        public const string WarningDark = "var(--rz-warning-dark)";

        /// <summary>
        /// Warning darker.
        /// </summary>
        public const string WarningDarker = "var(--rz-warning-darker)";

        /// <summary>
        /// Danger.
        /// </summary>
        public const string Danger = "var(--rz-danger)";

        /// <summary>
        /// Danger lighter.
        /// </summary>
        public const string DangerLighter = "var(--rz-danger-lighter)";

        /// <summary>
        /// Danger light.
        /// </summary>
        public const string DangerLight = "var(--rz-danger-light)";

        /// <summary>
        /// Danger dark.
        /// </summary>
        public const string DangerDark = "var(--rz-danger-dark)";

        /// <summary>
        /// Danger darker.
        /// </summary>
        public const string DangerDarker = "var(--rz-danger-darker)";

        /// <summary>
        /// White.
        /// </summary>
        public const string White = "var(--rz-white)";

        /// <summary>
        /// Black.
        /// </summary>
        public const string Black = "var(--rz-black)";

        /// <summary>
        /// Base 50.
        /// </summary>
        public const string Base50 = "var(--rz-base-50)";

        /// <summary>
        /// Base 100.
        /// </summary>
        public const string Base100 = "var(--rz-base-100)";

        /// <summary>
        /// Base 200.
        /// </summary>
        public const string Base200 = "var(--rz-base-200)";

        /// <summary>
        /// Base 300.
        /// </summary>
        public const string Base300 = "var(--rz-base-300)";

        /// <summary>
        /// Base 400.
        /// </summary>
        public const string Base400 = "var(--rz-base-400)";

        /// <summary>
        /// Base 500.
        /// </summary>
        public const string Base500 = "var(--rz-base-500)";

        /// <summary>
        /// Base 600.
        /// </summary>
        public const string Base600 = "var(--rz-base-600)";

        /// <summary>
        /// Base 700.
        /// </summary>
        public const string Base700 = "var(--rz-base-700)";

        /// <summary>
        /// Base 800.
        /// </summary>
        public const string Base800 = "var(--rz-base-800)";

        /// <summary>
        /// Base 900.
        /// </summary>
        public const string Base900 = "var(--rz-base-900)";

        /// <summary>
        /// Series1.
        /// </summary>
        public const string Series1 = "var(--rz-series-1)";

        /// <summary>
        /// Series2.
        /// </summary>
        public const string Series2 = "var(--rz-series-2)";

        /// <summary>
        /// Series3.
        /// </summary>
        public const string Series3 = "var(--rz-series-3)";

        /// <summary>
        /// Series4.
        /// </summary>
        public const string Series4 = "var(--rz-series-4)";

        /// <summary>
        /// Series5.
        /// </summary>
        public const string Series5 = "var(--rz-series-5)";

        /// <summary>
        /// Series6.
        /// </summary>
        public const string Series6 = "var(--rz-series-6)";

        /// <summary>
        /// Series7.
        /// </summary>
        public const string Series7 = "var(--rz-series-7)";

        /// <summary>
        /// Series8.
        /// </summary>
        public const string Series8 = "var(--rz-series-8)";
    }


    /// <summary>
    /// Represents orientation of components that have items.
    /// </summary>
    public enum Orientation
    {
        /// <summary>
        /// Sibling items are displayed next to each other.
        /// </summary>
        Horizontal,
        /// <summary>
        /// Sibling items are displayed below each other.
        /// </summary>
        Vertical
    }

    /// <summary>
    /// Specifies text alignment. Usually rendered as CSS <c>text-align</c> attribute.
    /// </summary>
    public enum TextAlign
    {
        /// <summary>
        /// The text is aligned to the left side of its container.
        /// </summary>
        Left,
        /// <summary>
        /// The text is aligned to the right side of its container.
        /// </summary>
        Right,
        /// <summary>
        /// The text is centered in its container.
        /// </summary>
        Center,
        /// <summary>The text is justified.</summary>
        Justify,
        /// <summary>Same as justify, but also forces the last line to be justified.</summary>
        JustifyAll,
        /// <summary>The same as left if direction is left-to-right and right if direction is right-to-left..</summary>
        Start,
        /// <summary>The same as right if direction is left-to-right and left if direction is right-to-left..</summary>
        End
    }

    /// <summary>
    /// Specifies horizontal alignment.
    /// </summary>
    public enum HorizontalAlign
    {
        /// <summary>
        /// Left horizontal alignment.
        /// </summary>
        Left,
        /// <summary>
        /// Right horizontal alignment.
        /// </summary>
        Right,
        /// <summary>
        /// Center horizontal alignment.
        /// </summary>
        Center,
        /// <summary>
        /// Justify horizontal alignment.
        /// </summary>
        Justify
    }

    /// <summary>
    /// Specifies the severity of a <see cref="RadzenNotification" />. Severity changes the visual styling of the RadzenNotification (icon and background color).
    /// </summary>
    public enum NotificationSeverity
    {
        /// <summary>
        /// Represents an error.
        /// </summary>
        Error,
        /// <summary>
        /// Represents some generic information.
        /// </summary>
        Info,
        /// <summary>
        /// Represents a success.
        /// </summary>
        Success,
        /// <summary>
        /// Represents a warning.
        /// </summary>
        Warning
    }

    /// <summary>
    /// Represents content justification of Stack items.
    /// </summary>
    public enum JustifyContent
    {
        /// <summary>
        /// Normal content justification of Stack items.
        /// </summary>
        Normal,
        /// <summary>
        /// Center content justification of Stack items.
        /// </summary>
        Center,
        /// <summary>
        /// Start content justification of Stack items.
        /// </summary>
        Start,
        /// <summary>
        /// End content justification of Stack items.
        /// </summary>
        End,
        /// <summary>
        /// Left content justification of Stack items.
        /// </summary>
        Left,
        /// <summary>
        /// Right content justification of Stack items.
        /// </summary>
        Right,
        /// <summary>
        /// SpaceBetween content justification of Stack items.
        /// </summary>
        SpaceBetween,
        /// <summary>
        /// SpaceAround content justification of Stack items.
        /// </summary>
        SpaceAround,
        /// <summary>
        /// SpaceEvenly content justification of Stack items.
        /// </summary>
        SpaceEvenly,
        /// <summary>
        /// Stretch content justification of Stack items.
        /// </summary>
        Stretch
    }

    /// <summary>
    /// Specifies the type of a <see cref="RadzenButton" />. Renders as the <c>type</c> HTML attribute.
    /// </summary>
    public enum ButtonType
    {
        /// <summary>
        /// Generic button which does not submit its parent form.
        /// </summary>
        Button,
        /// <summary>
        /// Clicking a submit button submits its parent form.
        /// </summary>
        Submit,
        /// <summary>
        /// Clicking a reset button clears the value of all inputs in its parent form.
        /// </summary>
        Reset
    }
    /// <summary>
    /// Specifies the size of a <see cref="RadzenButton" />.
    /// </summary>
    public enum ButtonSize
    {
        /// <summary>
        /// The default size of a button.
        /// </summary>
        Medium,
        /// <summary>
        /// A button larger than the default.
        /// </summary>
        Large,
        /// <summary>
        /// A button smaller than the default.
        /// </summary>
        Small,
        /// <summary>
        /// The smallest button.
        /// </summary>
        ExtraSmall
    }

    /// <summary>
    /// Specifies the display style of a <see cref="RadzenButton" />. Affects the visual styling of RadzenButton (background and text color).
    /// </summary>
    public enum ButtonStyle
    {
        /// <summary>
        /// A primary button. Clicking it performs the primary action in a form or dialog (e.g. "save").
        /// </summary>
        Primary,
        /// <summary>
        /// A secondary button. Clicking it performs a secondaryprimary action in a form or dialog (e.g. close a dialog or cancel a form).
        /// </summary>
        Secondary,
        /// <summary>
        /// A button with lighter styling.
        /// </summary>
        Light,
        /// <summary>
        /// The base UI styling.
        /// </summary>
        Base,
        /// <summary>
        /// A button with dark styling.
        /// </summary>
        Dark,
        /// <summary>
        /// A button with success styling.
        /// </summary>
        Success,
        /// <summary>
        /// A button which represents a dangerous action e.g. "delete".
        /// </summary>
        Danger,
        /// <summary>
        /// A button with warning styling.
        /// </summary>
        Warning,
        /// <summary>
        /// A button with informative styling.
        /// </summary>
        Info
    }
    /// <summary>
    /// Specifies the color shade of a <see cref="RadzenButton" />. Affects the visual styling of RadzenButton.
    /// </summary>
    public enum Shade
    {
        /// <summary>
        /// A button with lighter styling.
        /// </summary>
        Lighter,
        /// <summary>
        /// A button with light styling.
        /// </summary>
        Light,
        /// <summary>
        /// A button with default styling.
        /// </summary>
        Default,
        /// <summary>
        /// A button with dark styling.
        /// </summary>
        Dark,
        /// <summary>
        /// A button with darker styling.
        /// </summary>
        Darker
    }

    /// <summary>
    /// A base class of components that have child content.
    /// </summary>
    public class RadzenComponentWithChildren : RadzenComponent
    {
        /// <summary>
        /// Gets or sets the child content
        /// </summary>
        /// <value>The content of the child.</value>
        [Parameter]
        public RenderFragment ChildContent { get; set; }
    }

    /// <summary>
    /// Represents the alignment of Stack items.
    /// </summary>
    public enum AlignItems
    {
        /// <summary>
        /// Normal items alignment.
        /// </summary>
        Normal,
        /// <summary>
        /// Center items alignment.
        /// </summary>
        Center,
        /// <summary>
        /// Start items alignment.
        /// </summary>
        Start,
        /// <summary>
        /// End items alignment.
        /// </summary>
        End,
        /// <summary>
        /// Stretch items alignment.
        /// </summary>
        Stretch
    }

    /// <summary>
    /// A base class of row/col components.
    /// </summary>
    public class RadzenFlexComponent : RadzenComponentWithChildren
    {
        /// <summary>
        /// Gets or sets the content justify.
        /// </summary>
        /// <value>The content justify.</value>
        [Parameter]
        public JustifyContent JustifyContent { get; set; } = JustifyContent.Normal;

        /// <summary>
        /// Gets or sets the items alignment.
        /// </summary>
        /// <value>The items alignment.</value>
        [Parameter]
        public AlignItems AlignItems { get; set; } = AlignItems.Normal;

        internal string GetFlexCSSClass<T>(Enum v)
        {
            var value = ToDashCase(Enum.GetName(typeof(T), v));
            return value == "start" || value == "end" ? $"flex-{value}" : value;
        }

        internal string ToDashCase(string value)
        {
            var sb = new StringBuilder();

            foreach (var ch in value)
            {
                if ((char.IsUpper(ch) && sb.Length > 0) || char.IsSeparator(ch))
                {
                    sb.Append('-');
                }

                if (char.IsLetterOrDigit(ch))
                {
                    sb.Append(char.ToLowerInvariant(ch));
                }
            }

            return sb.ToString();
        }
    }

    /// <summary>
    /// Specifies the interface that form components must implement in order to be supported by <see cref="RadzenTemplateForm{TItem}" />.
    /// </summary>
    public interface IRadzenFormComponent
    {
        /// <summary>
        /// Gets a value indicating whether this component is bound.
        /// </summary>
        /// <value><c>true</c> if this component is bound; otherwise, <c>false</c>.</value>
        bool IsBound { get; }
        /// <summary>
        /// Gets a value indicating whether the component has value.
        /// </summary>
        /// <value><c>true</c> if this instance has value; otherwise, <c>false</c>.</value>
        bool HasValue { get; }

        /// <summary>
        /// Gets the value of the component.
        /// </summary>
        /// <returns>the value of the component - for example the text of RadzenTextBox.</returns>
        object GetValue();

        /// <summary>
        /// Gets or sets the name of the component.
        /// </summary>
        /// <value>The name.</value>
        string Name { get; set; }

        /// <summary>
        /// Gets the field identifier.
        /// </summary>
        /// <value>The field identifier.</value>
        FieldIdentifier FieldIdentifier { get; }

        /// <summary>
        /// Sets the focus.
        /// </summary>
        ValueTask FocusAsync();

        /// <summary>
        /// Sets the Disabled state of the component
        /// </summary>
        bool Disabled { get; set; }

        /// <summary>
        /// Sets the Visible state of the component
        /// </summary>
        bool Visible { get; set; }

        /// <summary>
        /// Sets the FormFieldContext of the component
        /// </summary>
        IFormFieldContext FormFieldContext { get; }
    }

    /// <summary>
    /// The interface that a validator component must implement in order to be supported by <see cref="RadzenTemplateForm{TItem}" />.
    /// </summary>
    public interface IRadzenFormValidator
    {
        /// <summary>
        /// Returns true if valid.
        /// </summary>
        /// <value><c>true</c> if the validator is valid; otherwise, <c>false</c>.</value>
        bool IsValid { get; }
        /// <summary>
        /// Gets or sets the name of the component which is validated.
        /// </summary>
        /// <value>The component name.</value>
        string Component { get; set; }
        /// <summary>
        /// Gets or sets the validation error displayed when invalid.
        /// </summary>
        /// <value>The text to display when invalid.</value>
        string Text { get; set; }
    }

    /// <summary>
    /// Specifies the design variant of <see cref="RadzenButton" /> . Affects the visual styling of RadzenButton and RadzenBadge.
    /// </summary>
    public enum Variant
    {
        /// <summary>
        /// A filled appearance.
        /// </summary>
        Filled,
        /// <summary>
        /// A flat appearance without any drop shadows.
        /// </summary>
        Flat,
        /// <summary>
        /// A text appearance.
        /// </summary>
        Text,
        /// <summary>
        /// An outlined appearance.
        /// </summary>
        Outlined
    }

    /// <summary>
    /// Represents whether items are forced onto one line or can wrap onto multiple lines.
    /// </summary>
    public enum FlexWrap
    {
        /// <summary>
        /// The items are laid out in a single line.
        /// </summary>
        NoWrap,
        /// <summary>
        /// The items break into multiple lines.
        /// </summary>
        Wrap,
        /// <summary>
        /// The items break into multiple lines reversed.
        /// </summary>
        WrapReverse
    }


    /// <summary>
    /// Represents the common <see cref="RadzenTemplateForm{TItem}" /> API used by
    /// its items. Injected as a cascading property in <see cref="IRadzenFormComponent" />.
    /// </summary>
    public interface IRadzenForm
    {
        /// <summary>
        /// Adds the specified component to the form.
        /// </summary>
        /// <param name="component">The component to add to the form.</param>
        void AddComponent(IRadzenFormComponent component);
        /// <summary>
        /// Removes the component from the form.
        /// </summary>
        /// <param name="component">The component to remove from the form.</param>
        void RemoveComponent(IRadzenFormComponent component);
        /// <summary>
        /// Finds a form component by its name.
        /// </summary>
        /// <param name="name">The name.</param>
        /// <returns>The component whose <see cref="IRadzenFormComponent.Name" /> equals to <paramref name="name" />; <c>null</c> if such a component is not found.</returns>
        IRadzenFormComponent FindComponent(string name);
    }

    /// <summary>
    /// Supplies information about a <see cref="RadzenTemplateForm{TItem}.InvalidSubmit" /> event that is being raised.
    /// </summary>
    public class FormInvalidSubmitEventArgs
    {
        /// <summary>
        /// Gets the validation errors.
        /// </summary>
        public IEnumerable<string> Errors { get; set; }
    }

    class Debouncer
    {
        System.Timers.Timer timer;
        DateTime timerStarted { get; set; } = DateTime.UtcNow.AddYears(-1);

        public void Debounce(int interval, Func<Task> action)
        {
            timer?.Stop();
            timer = null;

            timer = new System.Timers.Timer() { Interval = interval, Enabled = false, AutoReset = false };
            timer.Elapsed += (s, e) =>
            {
                if (timer == null)
                {
                    return;
                }

                timer?.Stop();
                timer = null;

                try
                {
                    Task.Run(action);
                }
                catch (TaskCanceledException)
                {
                    //
                }
            };

            timer.Start();
        }

        public void Throttle(int interval, Func<Task> action)
        {
            timer?.Stop();
            timer = null;

            var curTime = DateTime.UtcNow;

            if (curTime.Subtract(timerStarted).TotalMilliseconds < interval)
            {
                interval -= (int)curTime.Subtract(timerStarted).TotalMilliseconds;
            }

            timer = new System.Timers.Timer() { Interval = interval, Enabled = false, AutoReset = false };
            timer.Elapsed += (s, e) =>
            {
                if (timer == null)
                {
                    return;
                }

                timer?.Stop();
                timer = null;

                try
                {
                    Task.Run(action);
                }
                catch (TaskCanceledException)
                {
                    //
                }
            };

            timer.Start();
            timerStarted = curTime;
        }
    }

    /// <summary>
    /// Contains extension methods for <see cref="ParameterView" />.
    /// </summary>
    public static class ParameterViewExtensions
    {
        /// <summary>
        /// Checks if a parameter changed.
        /// </summary>
        /// <typeparam name="T">The value type</typeparam>
        /// <param name="parameters">The parameters.</param>
        /// <param name="parameterName">Name of the parameter.</param>
        /// <param name="parameterValue">The parameter value.</param>
        /// <returns><c>true</c> if the parameter value has changed, <c>false</c> otherwise.</returns>
        public static bool DidParameterChange<T>(this ParameterView parameters, string parameterName, T parameterValue)
        {
            if (parameters.TryGetValue(parameterName, out T value))
            {
                return !EqualityComparer<T>.Default.Equals(value, parameterValue);
            }

            return false;
        }
    }

    /// <summary>
    /// Utility class that provides property access based on strings.
    /// </summary>
    public static class PropertyAccess
    {
        /// <summary>
        /// Creates a function that will return the specified property.
        /// </summary>
        /// <typeparam name="TItem">The owner type.</typeparam>
        /// <typeparam name="TValue">The value type.</typeparam>
        /// <param name="propertyName">Name of the property to return.</param>
        /// <param name="type">Type of the object.</param>
        /// <returns>A function which return the specified property by its name.</returns>
        public static Func<TItem, TValue> Getter<TItem, TValue>(string propertyName, Type type = null)
        {
            if (propertyName.Contains("["))
            {
                var arg = Expression.Parameter(typeof(TItem));

                return Expression.Lambda<Func<TItem, TValue>>(QueryableExtension.GetNestedPropertyExpression(arg, propertyName, type), arg).Compile();
            }
            else
            {
                var arg = Expression.Parameter(typeof(TItem));

                Expression body = arg;

                if (type != null)
                {
                    body = Expression.Convert(body, type);
                }

                foreach (var member in propertyName.Split("."))
                {
                    if (body.Type.IsInterface)
                    {
                        body = Expression.Property(body,
                            new[] { body.Type }.Concat(body.Type.GetInterfaces()).FirstOrDefault(t => t.GetProperty(member) != null),
                            member
                        );
                    }
                    else
                    {
                        try
                        {
                            body = Expression.PropertyOrField(body, member);
                        }
                        catch (AmbiguousMatchException)
                        {
                            var property = body.Type.GetProperty(member, BindingFlags.Instance | BindingFlags.Public | BindingFlags.FlattenHierarchy);

                            if (property != null)
                            {
                                body = Expression.Property(body, property);
                            }
                            else
                            {
                                var field = body.Type.GetField(member, BindingFlags.Instance | BindingFlags.Public | BindingFlags.FlattenHierarchy);

                                if (field != null)
                                {
                                    body = Expression.Field(body, field);
                                }
                            }
                        }
                    }
                }

                body = Expression.Convert(body, typeof(TValue));

                return Expression.Lambda<Func<TItem, TValue>>(body, arg).Compile();
            }
        }

        /// <summary>
        /// Determines whether the specified type is a <see cref="DateTime" />.
        /// </summary>
        /// <param name="source">The source.</param>
        /// <returns><c>true</c> if the specified type is a DateTime instance or nullable DateTime; otherwise, <c>false</c>.</returns>
        public static bool IsDate(Type source)
        {
            if (source == null) return false;
            var type = source.IsGenericType ? source.GetGenericArguments()[0] : source;

            if (type == typeof(DateTime) || type == typeof(DateTimeOffset))
            {
                return true;
            }
#if NET6_0_OR_GREATER
            if (type == typeof(DateOnly))
            {
                return true;
            }
#endif
            return false;
        }

        /// <summary>
        /// Determines whether the specified type is a DateOnly.
        /// </summary>
        /// <param name="source">The source.</param>
        /// <returns><c>true</c> if the specified type is a DateOnly instance or nullable DateOnly; otherwise, <c>false</c>.</returns>
        public static bool IsDateOnly(Type source)
        {
            if (source == null) return false;
            var type = source.IsGenericType ? source.GetGenericArguments()[0] : source;

#if NET6_0_OR_GREATER
            if (type == typeof(DateOnly))
            {
                return true;
            }
#endif
            return false;
        }

        /// <summary>
        /// Determines whether the specified type is a DateOnly.
        /// </summary>
        /// <param name="source">The source.</param>
        /// <returns><c>true</c> if the specified type is a DateOnly instance or nullable DateOnly; otherwise, <c>false</c>.</returns>
        public static object DateOnlyFromDateTime(DateTime source)
        {
            object result = null;
#if NET6_0_OR_GREATER
            result = DateOnly.FromDateTime(source);
#endif
            return result;
        }

        /// <summary>
        /// Gets the type of the element of a collection time.
        /// </summary>
        /// <param name="type">The type.</param>
        /// <returns>The type of the collection element.</returns>
        public static Type GetElementType(Type type)
        {
            if (type.IsArray)
            {
                return type.GetElementType();
            }

            if (type.IsGenericType && type.GetGenericTypeDefinition() == typeof(IEnumerable<>))
            {
                return type.GetGenericArguments()[0];
            }

            var enumType = type.GetInterfaces()
                                    .Where(t => t.IsGenericType &&
                                           t.GetGenericTypeDefinition() == typeof(IEnumerable<>))
                                    .Select(t => t.GenericTypeArguments[0]).FirstOrDefault();
            return enumType ?? type;
        }

        /// <summary>
        /// Converts the property to a value that can be used by Dynamic LINQ.
        /// </summary>
        /// <param name="property">The property.</param>
        public static string GetProperty(string property)
        {
            return property;
        }

        /// <summary>
        /// Gets the value of the specified expression via reflection.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <param name="path">The path.</param>
        /// <returns>The value of the specified expression or <paramref name="value"/> if not found.</returns>
        public static object GetValue(object value, string path)
        {
            Type currentType = value.GetType();

            foreach (string propertyName in path.Split('.'))
            {
                var property = currentType.GetProperty(propertyName);
                if (property != null)
                {
                    if (value != null)
                    {
                        value = property.GetValue(value, null);
                    }

                    currentType = property.PropertyType;
                }
            }
            return value;
        }
        /// <summary>
        /// Creates a function that returns the specified property.
        /// </summary>
        /// <typeparam name="T">The value type</typeparam>
        /// <param name="data">The value.</param>
        /// <param name="propertyName">The name of the property to return.</param>
        /// <returns>A function that returns the specified property.</returns>
        public static Func<object, T> Getter<T>(object data, string propertyName)
        {
            var type = data.GetType();
            var arg = Expression.Parameter(typeof(object));
            var body = Expression.Convert(Expression.Property(Expression.Convert(arg, type), propertyName), typeof(T));

            return Expression.Lambda<Func<object, T>>(body, arg).Compile();
        }

        /// <summary>
        /// Tries to get a property by its name.
        /// </summary>
        /// <typeparam name="T">The target type</typeparam>
        /// <param name="item">The item.</param>
        /// <param name="property">The property.</param>
        /// <param name="result">The property value.</param>
        /// <returns><c>true</c> if successful, <c>false</c> otherwise.</returns>
        public static bool TryGetItemOrValueFromProperty<T>(object item, string property, out T result)
        {
            object r = GetItemOrValueFromProperty(item, property);

            if (r != null)
            {
                result = (T)r;
                return true;
            }
            else
            {
                result = default;
                return false;
            }

        }

        /// <summary>
        /// Gets the item or value from property.
        /// </summary>
        /// <param name="item">The item.</param>
        /// <param name="property">The property.</param>
        /// <returns>System.Object.</returns>
        public static object GetItemOrValueFromProperty(object item, string property)
        {
            if (item == null)
            {
                return null;
            }

            if (Convert.GetTypeCode(item) != TypeCode.Object || string.IsNullOrEmpty(property))
            {
                return item;
            }

            return PropertyAccess.GetValue(item, property);
        }

        /// <summary>
        /// Determines whether the specified type is numeric.
        /// </summary>
        /// <param name="source">The type.</param>
        /// <returns><c>true</c> if the specified source is numeric; otherwise, <c>false</c>.</returns>
        public static bool IsNumeric(Type source)
        {
            if (source == null)
                return false;

            var type = source.IsGenericType ? source.GetGenericArguments()[0] : source;

            switch (Type.GetTypeCode(type))
            {
                case TypeCode.Byte:
                case TypeCode.SByte:
                case TypeCode.UInt16:
                case TypeCode.UInt32:
                case TypeCode.UInt64:
                case TypeCode.Int16:
                case TypeCode.Int32:
                case TypeCode.Int64:
                case TypeCode.Decimal:
                case TypeCode.Double:
                case TypeCode.Single:
                    return true;
                default:
                    return false;
            }
        }

        /// <summary>
        /// Determines whether the specified type is an enum.
        /// </summary>
        /// <param name="source">The type.</param>
        /// <returns><c>true</c> if the specified source is an enum; otherwise, <c>false</c>.</returns>
        public static bool IsEnum(Type source)
        {
            if (source == null)
                return false;

            return source.IsEnum;
        }

        /// <summary>
        /// Determines whether the specified type is a Nullable enum.
        /// </summary>
        /// <param name="source">The type.</param>
        /// <returns><c>true</c> if the specified source is an enum; otherwise, <c>false</c>.</returns>
        public static bool IsNullableEnum(Type source)
        {
            if (source == null) return false;
            Type u = Nullable.GetUnderlyingType(source);
            return (u != null) && u.IsEnum;
        }

        /// <summary>
        /// Determines whether the specified type is anonymous.
        /// </summary>
        /// <param name="type">The type.</param>
        /// <returns><c>true</c> if the specified type is anonymous; otherwise, <c>false</c>.</returns>
        public static bool IsAnonymous(this Type type)
        {
            if (type.IsGenericType)
            {
                var d = type.GetGenericTypeDefinition();
                if (d.IsClass && d.IsSealed && d.Attributes.HasFlag(System.Reflection.TypeAttributes.NotPublic))
                {
                    var attributes = d.GetCustomAttributes(typeof(System.Runtime.CompilerServices.CompilerGeneratedAttribute), false);
                    if (attributes != null && attributes.Length > 0)
                    {
                        return true;
                    }
                }
            }
            return false;
        }

        /// <summary>
        /// Method to only replace first occurence of a substring in a string
        /// </summary>
        /// <param name="text"></param>
        /// <param name="search"></param>
        /// <param name="replace"></param>
        /// <returns></returns>
        public static string ReplaceFirst(this string text, string search, string replace)
        {
            int pos = text.IndexOf(search, StringComparison.Ordinal);
            if (pos < 0)
            {
                return text;
            }
            return text.Substring(0, pos) + replace + text.Substring(pos + search.Length);
        }

        /// <summary>
        /// Gets the type of the property.
        /// </summary>
        /// <param name="type">The type.</param>
        /// <param name="property">The property.</param>
        /// <returns>Type.</returns>
        public static Type GetPropertyType(Type type, string property)
        {
            if (property.Contains("."))
            {
                var part = property.Split('.').FirstOrDefault();
                return GetPropertyType(GetPropertyTypeIncludeInterface(type, part), property.ReplaceFirst($"{part}.", ""));
            }

            return GetPropertyTypeIncludeInterface(type, property);
        }

        private static Type GetPropertyTypeIncludeInterface(Type type, string property)
        {
            if (type != null)
            {
                return !type.IsInterface ?
                    type.GetProperty(property)?.PropertyType :
                        new Type[] { type }
                        .Concat(type.GetInterfaces())
                        .FirstOrDefault(t => t.GetProperty(property) != null)?
                        .GetProperty(property)?.PropertyType;
            }

            return null;
        }

        /// <summary>
        /// Gets the dynamic property expression when binding to IDictionary.
        /// </summary>
        /// <param name="name">The property name.</param>
        /// <param name="type">The property type.</param>
        /// <returns>Dynamic property expression.</returns>
        public static string GetDynamicPropertyExpression(string name, Type type)
        {
            var isEnum = type.IsEnum || Nullable.GetUnderlyingType(type)?.IsEnum == true;
            var typeName = isEnum ? "Enum" : (Nullable.GetUnderlyingType(type) ?? type).Name;
            var typeFunc = $@"{typeName}{(!isEnum && Nullable.GetUnderlyingType(type) != null ? "?" : "")}";

            return $@"({typeFunc})it[""{name}""]";
        }
    }

    /// <summary>
    /// Represents a filter in a component that supports filtering.
    /// </summary>
    public class FilterDescriptor
    {
        /// <summary>
        /// Gets or sets the name of the filtered property.
        /// </summary>
        /// <value>The property.</value>
        public string Property { get; set; }

        /// <summary>
        /// Gets or sets the property type.
        /// </summary>
        /// <value>The property type.</value>
        public Type Type { get; set; }

        /// <summary>
        /// Gets or sets the name of the filtered property.
        /// </summary>
        /// <value>The property.</value>
        public string FilterProperty { get; set; }
        /// <summary>
        /// Gets or sets the value to filter by.
        /// </summary>
        /// <value>The filter value.</value>
        public object FilterValue { get; set; }
        /// <summary>
        /// Gets or sets the operator which will compare the property value with <see cref="FilterValue" />.
        /// </summary>
        /// <value>The filter operator.</value>
        public FilterOperator FilterOperator { get; set; }
        /// <summary>
        /// Gets or sets a second value to filter by.
        /// </summary>
        /// <value>The second filter value.</value>
        public object SecondFilterValue { get; set; }
        /// <summary>
        /// Gets or sets the operator which will compare the property value with <see cref="SecondFilterValue" />.
        /// </summary>
        /// <value>The second filter operator.</value>
        public FilterOperator SecondFilterOperator { get; set; }
        /// <summary>
        /// Gets or sets the logic used to combine the outcome of filtering by <see cref="FilterValue" /> and <see cref="SecondFilterValue" />.
        /// </summary>
        /// <value>The logical filter operator.</value>
        public LogicalFilterOperator LogicalFilterOperator { get; set; }
    }

    /// <summary>
    /// Specifies the filter case sensitivity of a component.
    /// </summary>
    public enum FilterCaseSensitivity
    {
        /// <summary>
        /// Relies on the underlying provider (LINQ to Objects, Entity Framework etc.) to handle case sensitivity. LINQ to Objects is case sensitive. Entity Framework relies on the database collection settings.
        /// </summary>
        Default,
        /// <summary>
        /// Filters are case insensitive regardless of the underlying provider.
        /// </summary>
        CaseInsensitive
    }

    /// <summary>
    /// Specifies the comparison operator of a filter.
    /// </summary>
    public enum FilterOperator
    {
        /// <summary>
        /// Satisfied if the current value equals the specified value.
        /// </summary>
        Equals,
        /// <summary>
        /// Satisfied if the current value does not equal the specified value.
        /// </summary>
        NotEquals,
        /// <summary>
        /// Satisfied if the current value is less than the specified value.
        /// </summary>
        LessThan,
        /// <summary>
        /// Satisfied if the current value is less than or equal to the specified value.
        /// </summary>
        LessThanOrEquals,
        /// <summary>
        /// Satisfied if the current value is greater than the specified value.
        /// </summary>
        GreaterThan,
        /// <summary>
        /// Satisfied if the current value is greater than or equal to the specified value.
        /// </summary>
        GreaterThanOrEquals,
        /// <summary>
        /// Satisfied if the current value contains the specified value.
        /// </summary>
        Contains,
        /// <summary>
        /// Satisfied if the current value starts with the specified value.
        /// </summary>
        StartsWith,
        /// <summary>
        /// Satisfied if the current value ends with the specified value.
        /// </summary>
        EndsWith,
        /// <summary>
        /// Satisfied if the current value does not contain the specified value.
        /// </summary>
        DoesNotContain,
        /// <summary>
        /// Satisfied if the current value is in the specified value.
        /// </summary>
        In,
        /// <summary>
        /// Satisfied if the current value is not in the specified value.
        /// </summary>
        NotIn,
        /// <summary>
        /// Satisfied if the current value is null.
        /// </summary>
        IsNull,
        /// <summary>
        /// Satisfied if the current value is <see cref="string.Empty"/>.
        /// </summary>
        IsEmpty,
        /// <summary>
        /// Satisfied if the current value is not null.
        /// </summary>
        IsNotNull,
        /// <summary>
        /// Satisfied if the current value is not <see cref="string.Empty"/>.
        /// </summary>
        IsNotEmpty,
        /// <summary>
        /// Custom operator if not need to generate the filter.
        /// </summary>
        Custom
    }

    /// <summary>
    /// Specifies the logical operator between filters.
    /// </summary>
    public enum LogicalFilterOperator
    {
        /// <summary>
        /// All filters should be satisfied.
        /// </summary>
        And,
        /// <summary>
        /// Any filter should be satisfied.
        /// </summary>
        Or
    }

    /// <summary>
    /// Specifies the display style of a <see cref="RadzenIcon" />. Affects the visual styling of RadzenIcon (Icon (text) color).
    /// </summary>
    public enum IconStyle
    {
        /// <summary>
        /// Primary styling. Similar to primary buttons.
        /// </summary>
        Primary,
        /// <summary>
        /// Secondary styling. Similar to secondary buttons.
        /// </summary>
        Secondary,
        /// <summary>
        /// Light styling. Similar to light buttons.
        /// </summary>
        Light,
        /// <summary>
        /// Base styling. Similar to base buttons.
        /// </summary>
        Base,
        /// <summary>
        /// Dark styling. Similar to dark buttons.
        /// </summary>
        Dark,
        /// <summary>
        /// Success styling.
        /// </summary>
        Success,
        /// <summary>
        /// Danger styling.
        /// </summary>
        Danger,
        /// <summary>
        /// Warning styling.
        /// </summary>
        Warning,
        /// <summary>
        /// Informative styling.
        /// </summary>
        Info
    }

}
