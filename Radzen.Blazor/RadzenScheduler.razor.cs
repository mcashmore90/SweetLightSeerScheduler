using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Radzen.Blazor
{
    /// <summary>
    /// Displays a collection of <see cref="AppointmentData" /> in day, week or month view.
    /// </summary>
    /// <typeparam name="TItem">The type of the value item.</typeparam>
    /// <example>
    /// <code>
    /// &lt;RadzenScheduler Data="@data" TItem="DataItem" StartProperty="Start" EndProperty="End" TextProperty="Text"&gt;
    ///     &lt;RadzenMonthView /&gt;
    /// &lt;/RadzenScheduler&gt;
    /// @code {
    ///     class DataItem
    ///     {
    ///         public DateTime Start { get; set; }
    ///         public DateTime End { get; set; }
    ///         public string Text { get; set; }
    ///     }
    ///     DataItem[] data = new DataItem[]
    ///     {
    ///         new DataItem
    ///         {
    ///             Start = DateTime.Today,
    ///             End = DateTime.Today.AddDays(1),
    ///             Text = "Birthday"
    ///         },
    ///     };
    /// }
    /// </code>
    /// </example>
    public partial class RadzenScheduler<TItem> : RadzenComponent, IScheduler
    {
        /// <summary>
        /// Gets or sets the child content of the scheduler. Use to specify what views to render.
        /// </summary>
        /// <value>The child content.</value>
        [Parameter]
        public RenderFragment ChildContent { get; set; }

        /// <summary>
        /// Gets or sets the template used to render appointments.
        /// </summary>
        /// <example>
        /// <code>
        /// &lt;RadzenScheduler Data="@data" TItem="DataItem" StartProperty="Start" EndProperty="End" TextProperty="Text"&gt;
        ///    &lt;Template Context="data"&gt;
        ///       &lt;strong&gt;@data.Text&lt;/strong&gt;
        ///    &lt;/Template&gt;
        ///    &lt;ChildContent&gt;
        ///       &lt;RadzenMonthView /&gt;
        ///     &lt;/ChildContent&gt;
        /// &lt;/RadzenScheduler&gt;
        /// </code>
        /// </example>
        /// <value>The template.</value>
        [Parameter]
        public RenderFragment<TItem> Template { get; set; }

        /// <summary>
        /// Gets or sets the additional content to be rendered in place of the default navigation buttons in the scheduler.
        /// This property allows for complete customization of the navigation controls, replacing the native date navigation buttons (such as year, month, and day) with user-defined content or buttons.
        /// Use this to add custom controls or interactive elements that better suit your application's requirements.
        /// This requires that the <c>ShowHeader</c> parameter to be set to true (enabled by default).
        /// </summary>
        /// <value>The custom navigation template to replace default navigation buttons.</value>
        [Parameter]
        public RenderFragment NavigationTemplate { get; set; }


        /// <summary>
        /// Gets or sets the data of RadzenScheduler. It will display an appointment for every item of the collection which is within the current view date range.
        /// </summary>
        /// <value>The data.</value>
        [Parameter]
        public IEnumerable<TItem> Data { get; set; }

        /// <summary>
        /// Specifies the property of <typeparamref name="TItem" /> which will set <see cref="AppointmentData.Start" />.
        /// </summary>
        /// <value>The name of the property. Must be a <c>DateTime</c> property.</value>
        [Parameter]
        public string StartProperty { get; set; }

        /// <summary>
        /// Specifies the property of <typeparamref name="TItem" /> which will set <see cref="AppointmentData.End" />.
        /// </summary>
        /// <value>The name of the property. Must be a <c>DateTime</c> property.</value>
        [Parameter]
        public string EndProperty { get; set; }

        private int selectedIndex { get; set; }

        /// <summary>
        /// Specifies the initially selected view.
        /// </summary>
        /// <value>The index of the selected.</value>
        [Parameter]
        public int SelectedIndex { get; set; }

        /// <summary>
        /// Gets or sets the text of the today button. Set to <c>Today</c> by default.
        /// </summary>
        /// <value>The today text.</value>
        [Parameter]
        public string TodayText { get; set; } = "Today";

        /// <summary>
        /// Gets or sets the text of the next button. Set to <c>Next</c> by default.
        /// </summary>
        /// <value>The next text.</value>
        [Parameter]
        public string NextText { get; set; } = "Next";

        /// <summary>
        /// Gets or sets the text of the previous button. Set to <c>Previous</c> by default.
        /// </summary>
        /// <value>The previous text.</value>
        [Parameter]
        public string PrevText { get; set; } = "Previous";

        /// <summary>
        /// Gets or sets the initial date displayed by the selected view. Set to <c>DateTime.Today</c> by default.
        /// </summary>
        /// <value>The date.</value>
        [Parameter]
        public DateTime Date { get; set; } = DateTime.Today;

        /// <summary>
        /// Gets or sets the current date displayed by the selected view. Initially set to <see cref="Date" />. Changes during navigation.
        /// </summary>
        /// <value>The current date.</value>
        public DateTime CurrentDate { get; set; }

        /// <summary>
        /// Specifies the property of <typeparamref name="TItem" /> which will set <see cref="AppointmentData.Text" />.
        /// </summary>
        /// <value>The name of the property. Must be a <c>DateTime</c> property.</value>
        [Parameter]
        public string TextProperty { get; set; }

        /// <summary>
        /// Specifies whether to Show or Hide the Scheduler Header. Defaults to true />.
        /// </summary>
        /// <value>Show / hide header</value>
        [Parameter]
        public bool ShowHeader { get; set; } = true;

        /// <summary>
        /// A callback that will be invoked when the user clicks the Today button.
        /// </summary>
        /// <example>
        /// <code>
        /// &lt;RadzenScheduler Data=@appointments TodaySelect=@OnTodaySelect&gt;
        /// &lt;/RadzenScheduler&gt;
        /// @code {
        /// void OnTodaySelect(SchedulerTodaySelectEventArgs args)
        /// {
        ///     args.Today = DateTime.Today.AddDays(1);
        /// }
        /// }
        /// </code>
        /// </example>
        /// 
        [Parameter]
        public EventCallback<SchedulerTodaySelectEventArgs> TodaySelect { get; set; }
        
        
        /// A callback that will be invoked when the user clicks a month header button.
        /// <summary>
        /// <example>
        /// <code>
        /// &lt;RadzenScheduler Data=@appointments MonthSelect=@OnMonthSelect&gt;
        /// &lt;/RadzenScheduler&gt;
        /// @code {
        /// void OnMonthSelect(SchedulerMonthSelectEventArgs args)
        /// {
        ///     var selectedMonth = args.MonthStart.Month;
        /// }
        /// }
        /// </code>
        /// </example>
        /// 
        [Parameter]
        public EventCallback<SchedulerMonthSelectEventArgs> MonthSelect { get; set; }

        /// <summary>
        /// A callback that will be invoked when the user clicks an appointment in the current view. Commonly used to edit existing appointments.
        /// </summary>
        /// <example>
        /// <code>
        /// &lt;RadzenScheduler Data=@appointments AppointmentSelect=@OnAppointmentSelect&gt;
        /// &lt;/RadzenScheduler&gt;
        /// @code {
        ///  void OnAppointmentSelect(SchedulerAppointmentSelectEventArgs&lt;TItem&gt; args)
        ///  {
        ///  }
        /// }
        /// </code>
        /// </example>
        [Parameter]
        public EventCallback<SchedulerAppointmentSelectEventArgs<TItem>> AppointmentSelect { get; set; }

        /// <summary>
        /// An action that will be invoked when the current view renders an appointment. Never call <c>StateHasChanged</c> when handling AppointmentRender.
        /// </summary>
        /// <example>
        /// <code>
        /// &lt;RadzenScheduler Data=@appointments AppointmentRender=@OnAppointmentRendert&gt;
        /// &lt;/RadzenScheduler&gt;
        /// @code {
        ///   void OnAppintmentRender(SchedulerAppointmentRenderEventArgs&lt;TItem&gt; args)
        ///   {
        ///     if (args.Data.Text == "Birthday")
        ///     {
        ///        args.Attributes["style"] = "color: red;"
        ///     }
        ///.  }
        /// }
        /// </code>
        /// </example>
        [Parameter]
        public Action<SchedulerAppointmentRenderEventArgs<TItem>> AppointmentRender { get; set; }

        /// <summary>
        /// An action that will be invoked when the current view renders an slot. Never call <c>StateHasChanged</c> when handling SlotRender.
        /// </summary>
        /// <example>
        /// <code>
        /// &lt;RadzenScheduler Data=@appointments SlotRender=@OnSlotRender&gt;
        /// &lt;/RadzenScheduler&gt;
        /// @code {
        ///   void OnSlotRender(SchedulerSlotRenderEventArgs args)
        ///   {
        ///     if (args.View.Text == "Month" &amp;&amp; args.Start.Date == DateTime.Today)
        ///     {
        ///        args.Attributes["style"] = "background: red;";
        ///     }
        ///   }
        /// }
        /// </code>
        /// </example>
        [Parameter]
        public Action<SchedulerSlotRenderEventArgs> SlotRender { get; set; }

        /// <summary>
        /// A callback that will be invoked when the scheduler needs data for the current view. Commonly used to filter the
        /// data assigned to <see cref="Data" />.
        /// </summary>
        [Parameter]
        public EventCallback<SchedulerLoadDataEventArgs> LoadData { get; set; }

        IList<ISchedulerView> Views { get; set; } = new List<ISchedulerView>();

        /// <summary>
        /// Gets the SelectedView.
        /// </summary>
        public ISchedulerView SelectedView
        {
            get
            {
                return Views.ElementAtOrDefault(selectedIndex);
            }
        }

        /// <inheritdoc />
        public IDictionary<string, object> GetAppointmentAttributes(AppointmentData item)
        {
            var args = new SchedulerAppointmentRenderEventArgs<TItem> { Data = (TItem)item.Data, Start = item.Start, End = item.End };

            AppointmentRender?.Invoke(args);

            return args.Attributes;
        }

        /// <inheritdoc />
        public IDictionary<string, object> GetSlotAttributes(DateTime start, DateTime end, Func<IEnumerable<AppointmentData>> getAppointments)
        {
            var args = new SchedulerSlotRenderEventArgs { Start = start, End = end, getAppointments = getAppointments, View = SelectedView };

            SlotRender?.Invoke(args);

            return args.Attributes;
        }

        /// <inheritdoc />
        public RenderFragment RenderAppointment(AppointmentData item)
        {
            if (Template != null)
            {
                TItem context = (TItem)item.Data;
                return Template(context);
            }

            return builder => builder.AddContent(0, item.Text);
        }

        /// <inheritdoc />
        public async Task SelectMonth(DateTime monthStart, IEnumerable<AppointmentData> appointments)
        {
            await MonthSelect.InvokeAsync(new SchedulerMonthSelectEventArgs { MonthStart = monthStart, Appointments = appointments, View = SelectedView });
        }

        /// <inheritdoc />
        public async Task SelectAppointment(AppointmentData data)
        {
            await AppointmentSelect.InvokeAsync(new SchedulerAppointmentSelectEventArgs<TItem> { Start = data.Start, End = data.End, Data = (TItem)data.Data });
        }

        /// <inheritdoc />
        public async Task AddView(ISchedulerView view)
        {
            if (!Views.Contains(view))
            {
                Views.Add(view);

                if (SelectedView == view)
                {
                    await InvokeLoadData();
                }

                StateHasChanged();
            }
        }

        /// <summary>
        /// Selects the specified <see cref="ISchedulerView"/>. The view must already be present in this scheduler.
        /// If the specified view is already selected, no action will be performed.
        /// </summary>
        /// <param name="view">The <see cref="ISchedulerView"/> to select</param>
        public async Task SelectView(ISchedulerView view)
        {
            var viewIndex = Views.IndexOf(view);
            if (viewIndex == -1)
                return;

            if (SelectedView == view)
                return;

            selectedIndex = viewIndex;

            await InvokeLoadData();

            StateHasChanged();
        }

        /// <summary>
        /// Causes the current scheduler view to render. Enumerates the items of <see cref="Data" /> and creates instances of <see cref="AppointmentData" /> to
        /// display in the current view. Use it when <see cref="Data" /> has changed.
        /// </summary>
        public async Task Reload()
        {
            appointments = null;

            await InvokeLoadData();

            StateHasChanged();
        }

        /// <inheritdoc />
        public bool IsSelected(ISchedulerView view)
        {
            return selectedIndex == Views.IndexOf(view);
        }

        async Task OnChangeView(ISchedulerView view)
        {
            selectedIndex = Views.IndexOf(view);

            await InvokeLoadData();
        }

        async Task OnPrev()
        {
            CurrentDate = SelectedView.Prev();

            await InvokeLoadData();
        }

        async Task OnToday()
        {
            var args = new SchedulerTodaySelectEventArgs { Today = DateTime.Now.Date };

            await TodaySelect.InvokeAsync(args);

            CurrentDate = args.Today;

            await InvokeLoadData();
        }

        async Task OnNext()
        {
            CurrentDate = SelectedView.Next();

            await InvokeLoadData();
        }

        /// <inheritdoc />
        public void RemoveView(ISchedulerView view)
        {
            Views.Remove(view);
        }

        /// <inheritdoc />
        protected override void OnInitialized()
        {
            CurrentDate = Date;
            selectedIndex = SelectedIndex;

            double height = 0;

            var style = CurrentStyle;

            if (style.ContainsKey("height"))
            {
                var pixelHeight = style["height"];

                if (pixelHeight.EndsWith("px"))
                {
                    height = Convert.ToDouble(pixelHeight.TrimEnd("px".ToCharArray()));
                }
            }

            if (height > 0)
            {
                heightIsSet = true;

                Height = height;
            }
        }

        IEnumerable<AppointmentData> appointments;
        DateTime rangeStart;
        DateTime rangeEnd;
        Func<TItem, DateTime> startGetter;
        Func<TItem, DateTime> endGetter;
        Func<TItem, string> textGetter;

        /// <inheritdoc />
        public override async Task SetParametersAsync(ParameterView parameters)
        {
            var needsReload = false;

            if (parameters.DidParameterChange(nameof(Date), Date))
            {
                CurrentDate = parameters.GetValueOrDefault<DateTime>(nameof(Date));
                needsReload = true;
            }

            if (parameters.DidParameterChange(nameof(SelectedIndex), SelectedIndex))
            {
                selectedIndex = parameters.GetValueOrDefault<int>(nameof(SelectedIndex));
                needsReload = true;
            }

            if (parameters.DidParameterChange(nameof(Data), Data))
            {
                appointments = null;
            }

            if (parameters.DidParameterChange(nameof(StartProperty), StartProperty))
            {
                startGetter = PropertyAccess.Getter<TItem, DateTime>(parameters.GetValueOrDefault<string>(nameof(StartProperty)));
            }

            if (parameters.DidParameterChange(nameof(EndProperty), EndProperty))
            {
                endGetter = PropertyAccess.Getter<TItem, DateTime>(parameters.GetValueOrDefault<string>(nameof(EndProperty)));
            }

            if (parameters.DidParameterChange(nameof(TextProperty), TextProperty))
            {
                textGetter = PropertyAccess.Getter<TItem, string>(parameters.GetValueOrDefault<string>(nameof(TextProperty)));
            }

            await base.SetParametersAsync(parameters);

            if (needsReload)
            {
                await InvokeLoadData();
            }
        }

        private async Task InvokeLoadData()
        {
            if (SelectedView != null)
            {
                await LoadData.InvokeAsync(new SchedulerLoadDataEventArgs { Start = SelectedView.StartDate, End = SelectedView.EndDate, View = SelectedView });
            }
        }

        /// <inheritdoc />
        public bool IsAppointmentInRange(AppointmentData item, DateTime start, DateTime end)
        {
            if (item.Start == item.End && item.Start >= start && item.End < end)
            {
                return true;
            }

            return item.End > start && item.Start < end;
        }

        /// <inheritdoc />
        public IEnumerable<AppointmentData> GetAppointmentsInRange(DateTime start, DateTime end)
        {
            if (Data == null)
            {
                return [];
            }

            if (start == rangeStart && end == rangeEnd && appointments != null)
            {
                return appointments;
            }

            rangeStart = start;
            rangeEnd = end;

            appointments = Data.AsQueryable()
                               .Where([
                                    new FilterDescriptor { Property = EndProperty, FilterValue = start, FilterOperator = FilterOperator.GreaterThanOrEquals },
                                    new FilterDescriptor { Property = StartProperty, FilterValue = end, FilterOperator = FilterOperator.LessThanOrEquals }
                                ], LogicalFilterOperator.And, FilterCaseSensitivity.Default)
                               .ToList()
                               .Select(item => new AppointmentData { Start = startGetter(item), End = endGetter(item), Text = textGetter(item), Data = item });

            return appointments;
        }

        class Rect
        {
            public double Width { get; set; }
            public double Height { get; set; }
        }

        /// <inheritdoc />
        protected override async Task OnAfterRenderAsync(bool firstRender)
        {
            await base.OnAfterRenderAsync(firstRender);

            if (firstRender)
            {
                var rect = await JSRuntime.InvokeAsync<Rect>("Radzen.createScheduler", Element, Reference);

                if (!heightIsSet)
                {
                    heightIsSet = true;
                    Resize(rect.Width, rect.Height);
                }
            }
        }

        /// <summary>
        /// Invoked from client-side via interop when the scheduler size changes.
        /// </summary>
        /// <param name="width">The width.</param>
        /// <param name="height">The height.</param>
        [JSInvokable]
        public void Resize(double width, double height)
        {
            var stateHasChanged = false;

            if (height != Height)
            {
                Height = height;
                stateHasChanged = true;
            }

            if (stateHasChanged)
            {
                StateHasChanged();
            }
        }

        /// <inheritdoc />
        public override void Dispose()
        {
            base.Dispose();

            if (IsJSRuntimeAvailable)
            {
                JSRuntime.InvokeVoid("Radzen.destroyScheduler", Element);
            }
        }

        private bool heightIsSet = false;
        private double Height { get; set; } = 400; // Default height set from theme.
        double IScheduler.Height
        {
            get
            {
                return Height;
            }
        }
        /// <inheritdoc />
        protected override string GetComponentCssClass()
        {
            return $"rz-scheduler";
        }
    }
}
