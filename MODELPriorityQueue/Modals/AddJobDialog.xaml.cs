﻿using MODELPriorityQueue.Models;
using Windows.UI.Xaml.Controls;

// The Content Dialog item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace MODELPriorityQueue.Modals
{
    public sealed partial class AddJobDialog : ContentDialog
    {
        public AddJobDialog()
        {
            this.InitializeComponent();
            Opened += AddJobDialog_Opened;
        }

        private async void AddJobDialog_Opened(ContentDialog sender, ContentDialogOpenedEventArgs args)
        {
            CustomerBox.ItemsSource = await Customer.Get();
        }

        /// <summary>
        /// Close the modal window with no saving
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="args"></param>
        private void ContentDialog_PrimaryButtonClick(ContentDialog sender, ContentDialogButtonClickEventArgs args)
        {
        }

        /// <summary>
        /// Save and close the modal window
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="args"></param>
        private async void ContentDialog_SecondaryButtonClick(ContentDialog sender, ContentDialogButtonClickEventArgs args)
        {
            int numHours = 0;
            if (!string.IsNullOrEmpty(EstimatedHours.Text))
            {
                if (!int.TryParse(EstimatedHours.Text, out numHours))
                    return;
            }

            Customer c = CustomerBox.SelectedItem as Customer;
            if (c == null)
                return;

            Job job = new Job()
            {
                Subject = Subject.Text,
                Description = Description.Text,
                Hours = numHours,
                Customer = c.Id
            };

            await job.Post();
        }
    }
}
