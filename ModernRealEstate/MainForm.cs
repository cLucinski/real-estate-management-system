/**
 * Author: Christopher Lucinski
 * Course code: DA259E
 */

using RealEstateBLL;
using RealEstateDAL;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using UtilitiesLib;

namespace ModernRealEstate
{
    public partial class MainForm : Form
    {
        //Estate Manager to save and manipulate estates
        private readonly EstateManager estateManager = new EstateManager();
        //Initital blank estate to be manipulated
        private Estate estate;
        //Initial blank file to be manipulated
        private Image imageFile;
        //String to hold the filepath of any current file being worked on. Empty if no current file.
        private string liveFilepath = "";

        public MainForm()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Prepare the GUI with initializations.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void MainForm_Load(object sender, EventArgs e)
        {
            //Initialize Data Sources
            comboEstateType.DataSource = Enum.GetValues(typeof(EstateType));
            comboCountry.DataSource = Enum.GetValues(typeof(Countries));
            comboResEstateType.DataSource = Enum.GetValues(typeof(ResidentialEstateType));
            comboResLegalForm.DataSource = Enum.GetValues(typeof(LegalForm));
            comboCommEstateType.DataSource = Enum.GetValues(typeof(CommercialEstateType));
            comboInstEstateType.DataSource = Enum.GetValues(typeof(InstitutionalEstateType));
            comboEstateType.SelectedIndex = -1;
            
            //Tooltips
            toolTip1.SetToolTip(btnAddImage, "Add a picture to attach to the estate.");

            //Additional data sources 
            comboBuyerCountry.DataSource = Enum.GetValues(typeof(Countries));
            comboSellerCountry.DataSource = Enum.GetValues(typeof(Countries));
            comboPayType.DataSource = Enum.GetValues(typeof(PaymentType));

            UpdateGUI();

            return;
        }

        /// <summary>
        /// Resets the GUI's information fields.
        /// </summary>
        private void UpdateGUI()
        {
            //Clear all fields and enable them
            foreach (GroupBox c in Controls.OfType<GroupBox>())
            {
                ClearAllFields(c.Controls);
                SetEnableControls(c.Controls);
            }
            //Also clear and enable fields of tabBuyerSeller
            foreach (TabPage t in tabBuyerSeller.TabPages)
            {
                ClearAllFields(t.Controls);
                SetEnableControls(t.Controls);
            }

            pictureEstate.Image = null;

            //Update visible list
            listEstates.Items.Clear();
            listEstates.Items.AddRange(estateManager.ToStringArray());

            return;
        }

        #region User Controls

        /// <summary>
        /// Maniuplates visibility of GroupBoxes containing information for 
        /// different Estate Types.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ComboEstateTypeChanged(object sender, EventArgs e)
        {
            //Debug.WriteLine(comboEstateType.SelectedItem);

            groupPictureEstate.Visible = true;
            
            groupResidential.Visible = false;
            groupApartment.Visible = false;
            groupVilla.Visible = false;
            groupRowhouse.Visible = false;

            groupCommercial.Visible = false;
            groupShop.Visible = false;
            groupWarehouse.Visible = false;

            groupInstitutional.Visible = false;
            groupUniversity.Visible = false;
            groupSchool.Visible = false;
            groupHospital.Visible = false;

            comboResEstateType.SelectedIndex = -1;
            comboResLegalForm.SelectedIndex = -1;
            comboCommEstateType.SelectedIndex = -1;
            comboInstEstateType.SelectedIndex = -1;

            switch (comboEstateType.SelectedIndex)
            {
                case (int)EstateType.Residential:
                    groupResidential.Visible = true;
                    break;
                case (int)EstateType.Commercial:
                    groupCommercial.Visible = true;
                    break;
                case (int)EstateType.Institutional:
                    groupInstitutional.Visible = true;
                    break;
                default:
                    break;
            }
            return;
        }

        /// <summary>
        /// Change visibility of ResidentialEstateType GroupBoxes.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ComboResEstateTypeChanged(object sender, EventArgs e)
        {
            groupApartment.Visible = false;
            groupVilla.Visible = false;
            groupRowhouse.Visible = false;

            switch (comboResEstateType.SelectedIndex)
            {
                case (int)ResidentialEstateType.Apartment:
                    groupApartment.Visible = true;
                    groupAptLegalForm.Visible = false;
                    break;
                case (int)ResidentialEstateType.Villa:
                    groupVilla.Visible = true;
                    break;
                case (int)ResidentialEstateType.Rowhouse:
                    groupRowhouse.Visible = true;
                    break;
                default:
                    break;
            }
            return;
        }

        /// <summary>
        /// Change visibility of the visible LegalForm information.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ComboResLegalFormChanged(object sender, EventArgs e)
        {
            groupAptLegalForm.Visible = true;
            switch (comboResLegalForm.SelectedIndex)
            {
                case (int)LegalForm.Rental:
                    groupAptLegalForm.Text = "Rental Info";
                    checkAptHydroIncl.Visible = true;
                    break;
                case (int)LegalForm.Tenement:
                    groupAptLegalForm.Text = "Tenement Info";
                    checkAptHydroIncl.Visible = false;
                    break;
                case (int)LegalForm.Ownership:
                    groupAptLegalForm.Text = "Ownership Info";
                    checkAptHydroIncl.Visible = false;
                    break;
                default:
                    groupAptLegalForm.Text = "Legal Form Info";
                    checkAptHydroIncl.Visible = false;
                    break;
            }
            return;
        }

        /// <summary>
        /// Change visibility of CommercialEstateType GroupBoxes.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ComboCommEstateTypeChanged(object sender, EventArgs e)
        {
            groupShop.Visible = false;
            groupWarehouse.Visible = false;

            switch (comboCommEstateType.SelectedIndex)
            {
                case (int)CommercialEstateType.Shop:
                    groupShop.Visible = true;
                    break;
                case (int)CommercialEstateType.Warehouse:
                    groupWarehouse.Visible = true;
                    break;
                default:
                    break;
            }
            return;
        }

        /// <summary>
        /// Change visibility of InstitutionalEstateType GroupBoxes.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ComboInstEstateTypeChanged(object sender, EventArgs e)
        {
            groupUniversity.Visible = false;
            groupSchool.Visible = false;
            groupHospital.Visible = false;

            switch (comboInstEstateType.SelectedIndex)
            {
                case (int)InstitutionalEstateType.University:
                    groupUniversity.Visible = true;
                    break;
                case (int)InstitutionalEstateType.School:
                    groupSchool.Visible = true;
                    break;
                case (int)InstitutionalEstateType.Hospital:
                    groupHospital.Visible = true;
                    break;
                default:
                    break;
            }
            return;
        }

        /// <summary>
        /// Validate current information and save a new Estate.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnSave_Click(object sender, EventArgs e)
        {
            //Validate
            bool isValid = ValidateAllRelevantInputFields();
            
            if (!isValid)
            {
                Debug.WriteLine("Something ain't valid.");
            }
            else
            {
                CreateEstate(true);
                btnEdit.Enabled = false;
                btnSaveChanges.Enabled = false;
                btnCancel.Enabled = false;
                btnAddImage.Enabled = false;
            }

            return;
        }

        /// <summary>
        /// Change Enabled status of Estate list buttons when a list item is selected.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void listEstates_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (listEstates.SelectedIndex == -1)
            {
                btnDisplayEstate.Enabled = false;
                btnSaveChanges.Enabled = false;
                btnEdit.Enabled = false;
                btnDelete.Enabled = false;
                return;
            }

            btnDisplayEstate.Enabled = true;
            btnEdit.Enabled = true;
            btnDelete.Enabled = true;
            return;
        }

        /// <summary>
        /// Display the selected Estate.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnDisplayEstate_Click(object sender, EventArgs e)
        {
            Estate toDisplay = estateManager.GetAt(listEstates.SelectedIndex);

            DisplayEstate(toDisplay);

            foreach (GroupBox c in Controls.OfType<GroupBox>())
            {
                SetDisableControls(c.Controls);
            }
            //Also disable controls of tabBuyerSeller
            foreach (TabPage t in tabBuyerSeller.TabPages)
            {
                SetDisableControls(t.Controls);
            }

            btnEdit.Enabled = true;
            btnDelete.Enabled = true;
            btnAddImage.Enabled = false;

            return;
        }

        /// <summary>
        /// Open the selected Estate to be edited.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnEdit_Click(object sender, EventArgs e)
        {
            Estate toDisplay = estateManager.GetAt(listEstates.SelectedIndex);

            DisplayEstate(toDisplay);

            foreach (GroupBox c in Controls.OfType<GroupBox>())
            {
                SetEnableControls(c.Controls);
            }
            //Also enable controls of tabBuyerSeller
            foreach (TabPage t in tabBuyerSeller.TabPages)
            {
                SetEnableControls(t.Controls);
            }

            btnEdit.Enabled = false;
            btnSaveChanges.Enabled = true;
            btnCancel.Enabled = true;
            btnAddImage.Enabled = true;

            return;
        }

        /// <summary>
        /// Save any changes made to the current Estate being edited.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnSaveChanges_Click(object sender, EventArgs e)
        {
            bool isValid = ValidateAllRelevantInputFields();
            if (!isValid)
            {
                Debug.WriteLine("Something isn't valid.");
            }
            else if (CreateEstate(false))
            {
                btnEdit.Enabled = true;
                btnSaveChanges.Enabled = false;
                btnCancel.Enabled = false;
                btnAddImage.Enabled = false;
            }
            return;
        }

        /// <summary>
        /// Cancel editing and revert any changes made the Estate's fields.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnCancel_Click(object sender, EventArgs e)
        {
            btnDisplayEstate_Click(sender, e);

            btnEdit.Enabled = true;
            btnSaveChanges.Enabled = false;
            btnCancel.Enabled = false;
            btnAddImage.Enabled = false;
            return;
        }

        /// <summary>
        /// Delete the currently selected Estate from the list.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnDelete_Click(object sender, EventArgs e)
        {
            //Ask for confirmation
            var confirmResult = MessageBox.Show("Are you sure you wish to delete this estate entry? It cannot be undone.",
                                     "Confirm Deletion",
                                     MessageBoxButtons.YesNo);
            if (confirmResult == DialogResult.Yes)
            {
                //Remove selected estate
                estateManager.DeleteAt(listEstates.SelectedIndex);

                //Update list and GUI
                UpdateGUI();
            }
            else
            {
                // If 'No', do nothing.
            }
            return;
        }

        /// <summary>
        /// Clear all information fields and prep the GUI for a new Estate.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnClearAllFields_Click(object sender, EventArgs e)
        {
            //Ask for confirmation
            var confirmResult = MessageBox.Show("Are you sure you wish to clear information fields? Any unsaved data will be lost. This cannot be undone.",
                                     "Confirm Deletion",
                                     MessageBoxButtons.YesNo);
            if (confirmResult == DialogResult.Yes)
            {
                //Clear all fields
                foreach (GroupBox c in Controls.OfType<GroupBox>())
                {
                    ClearAllFields(c.Controls);
                    SetEnableControls(c.Controls);
                }
                //Also clear controls of tabBuyerSeller
                foreach (TabPage t in tabBuyerSeller.TabPages)
                {
                    ClearAllFields(t.Controls);
                    SetEnableControls(t.Controls);
                }
            }
            else
            {
                // If 'No', do nothing.
            }
            return;
        }

        /// <summary>
        /// Open a dialogue to add a locally saved image to the GUI.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnAddImage_Click(object sender, EventArgs e)
        {
            OpenFileDialog f = new OpenFileDialog
            {
                Filter = "Image Files|*.jpg;*.jpeg;*.png;"
            };
            if (f.ShowDialog() == DialogResult.OK)
            {
                imageFile = Image.FromFile(f.FileName);
                pictureEstate.Image = imageFile;
            }
        }

        /// <summary>
        /// Manipulate the visibility of Payment GroupBox controls.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void comboPayType_SelectedIndexChanged(object sender, EventArgs e)
        {
            switch (comboPayType.SelectedIndex)
            {
                case (int)PaymentType.Bank:
                    labelPay1.Visible = true; 
                    labelPay2.Visible = true;
                    txtPay1.Visible = true;
                    txtPay2.Visible = true;

                    labelPay1.Text = "Bank Name";
                    labelPay2.Text = "Account #";
                    txtPay1.Text = "";
                    txtPay2.Text = "";
                    break;
                case (int)PaymentType.Western_Union:
                    labelPay1.Visible = true;
                    labelPay2.Visible = false;
                    txtPay1.Visible = true;
                    txtPay2.Visible = false;

                    labelPay1.Text = "Account #";
                    txtPay1.Text = "";
                    break;
                case (int)PaymentType.PayPal:
                    labelPay1.Visible = true;
                    labelPay2.Visible = true;
                    txtPay1.Visible = true;
                    txtPay2.Visible = true;

                    labelPay1.Text = "Email";
                    labelPay2.Text = "Card #";
                    txtPay1.Text = "";
                    txtPay2.Text = "";
                    break;
                default:
                    labelPay1.Visible = false;
                    labelPay2.Visible = false;
                    txtPay1.Visible = false;
                    txtPay2.Visible = false;
                    break;
            }
            return;
        }

        /// <summary>
        /// Helper to clear controls of a ControlCollection.
        /// </summary>
        /// <param name="controlCollection"></param>
        private void ClearAllFields(Control.ControlCollection controlCollection)
        {
            if (controlCollection == null)
            {
                return;
            }
            foreach (ComboBox c in controlCollection.OfType<ComboBox>())
            {
                c.SelectedIndex = -1;
            }
            foreach (TextBoxBase c in controlCollection.OfType<TextBoxBase>())
            {
                c.Text = "";
            }
            foreach (CheckBox c in controlCollection.OfType<CheckBox>())
            {
                c.Checked = false;
            }

            pictureEstate.Image = null;
            
            return;
        }

        /// <summary>
        /// Helper to enable controls of a ControlCollection.
        /// </summary>
        /// <param name="controlCollection"></param>
        private void SetEnableControls(Control.ControlCollection controlCollection)
        {
            if (controlCollection == null)
            {
                return;
            }
            foreach (ComboBox c in controlCollection.OfType<ComboBox>())
            {
                c.Enabled = true;
            }
            foreach (TextBoxBase c in controlCollection.OfType<TextBoxBase>())
            {
                c.ReadOnly = false;
            }
            foreach (CheckBox c in controlCollection.OfType<CheckBox>())
            {
                c.Enabled = true;
            }

            btnAddImage.Enabled = true;

            return;
        }

        /// <summary>
        /// Helper to disable controls of a ControlCollection.
        /// </summary>
        /// <param name="controlCollection"></param>
        private void SetDisableControls(Control.ControlCollection controlCollection)
        {
            if (controlCollection == null)
            {
                return;
            }
            foreach (ComboBox c in controlCollection.OfType<ComboBox>())
            {
                c.Enabled = false;
            }
            foreach (TextBoxBase c in controlCollection.OfType<TextBoxBase>())
            {
                c.ReadOnly = true;
            }
            foreach (CheckBox c in controlCollection.OfType<CheckBox>())
            {
                c.Enabled = false;
            }

            btnAddImage.Enabled = false;
            
            return;
        }

        #endregion

        #region Estate Creation

        /// <summary>
        /// Create an Estate based on the information existing on the frontend.
        /// </summary>
        /// <param name="isNew">Marks whether the Estate is brand new,
        /// or is an already-existing one being edited and re-saved.</param>
        /// <returns>Returns true if the Estate was successfully created and added to the list.</returns>
        private bool CreateEstate(bool isNew)
        {
            //If a new estate, check for already-existing Estate with same ID
            if (isNew)
            {
                if (estateManager.CheckForEstateWithID(txtID.Text))
                {
                    MessageBox.Show("Error: Cannot save new estate; an estate with that ID already exists.", "Error");
                    return false;
                }
            }
            
            //Begin creation of Estate
            estate = null;
            EstateType estateType = (EstateType)comboEstateType.SelectedIndex;

            switch (estateType)
            {
                case EstateType.Residential:
                    CreateResidential();
                    break;
                case EstateType.Commercial:
                    CreateCommercial();
                    break;
                case EstateType.Institutional:
                    CreateInstitutional();
                    break;
                default:
                    Debug.WriteLine("Error: EstateType to create not found.");
                    break;
            }

            //If the estate isn't new, replace the old version with the updated one.
            if (!isNew)
            {
                //Add in existing place in list
                int currentIndex = listEstates.SelectedIndex;
                if (estateManager.ChangeAt(estate, currentIndex))
                {
                    //Return to displaying the newly updated estate
                    DisplayEditedEstate(estate);
                    listEstates.SelectedIndex = currentIndex;
                }
                else
                {
                    MessageBox.Show("Estate failed to be updated.", "Error");
                    return false;
                }
            }
            else  //Otherwise, add the new Estate to the end of the list
            {
                //Add to end of list
                estateManager.Add(estate);

                //Update visible list and fields
                UpdateGUI();
            }
            return true;
        }

        /// <summary>
        /// Display an Estate after it has been edited.
        /// </summary>
        /// <param name="editedEstate"></param>
        private void DisplayEditedEstate(Estate editedEstate)
        {
            DisplayEstate(editedEstate);

            foreach (GroupBox c in Controls.OfType<GroupBox>())
            {
                SetDisableControls(c.Controls);
            }
            //Also disable controls of tabBuyerSeller
            foreach (TabPage t in tabBuyerSeller.TabPages)
            {
                SetDisableControls(t.Controls);
            }

            //Update visible list
            listEstates.Items.Clear();
            listEstates.Items.AddRange(estateManager.ToStringArray());

            //Update buttons
            btnEdit.Enabled = true;
            btnSaveChanges.Enabled = false;
            btnCancel.Enabled = false;
            return;
        }

        //Residential 

        /// <summary>
        /// Create a Residential estate.
        /// </summary>
        private void CreateResidential()
        {
            //Determine which Residential object to create
            ResidentialEstateType resEstateType = (ResidentialEstateType)comboResEstateType.SelectedIndex;
            switch (resEstateType)
            {
                case ResidentialEstateType.Apartment:
                    CreateApartment();
                    break;
                case ResidentialEstateType.Rowhouse:
                    CreateRowhouse();
                    break;
                case ResidentialEstateType.Villa:
                    CreateVilla();
                    break;
                default:
                    Debug.WriteLine("Error: ResEstateType to create not found.");
                    break;
            }
            return;
        }

        /// <summary>
        /// Create an Apartment.
        /// </summary>
        private void CreateApartment()
        {
            //Determine which Apartment object to create
            LegalForm resLegalForm = (LegalForm)comboResLegalForm.SelectedIndex;
            switch (resLegalForm)
            {
                case LegalForm.Rental:
                    CreateRental();
                    break;
                case LegalForm.Tenement:
                    CreateTenement();
                    break;
                case LegalForm.Ownership:
                    CreateTenement();  //This is done on purpose because an Ownership is the same as a Tenement
                    break;
                default:
                    Debug.WriteLine("Error: ResEstateType to create not found.");
                    break;
            }
            return;
        }

        /// <summary>
        /// Helper method to save Estate information which doesn't need to be typecasted.
        /// </summary>
        private void SaveBaseEstateInfo()
        {
            estate.ID = txtID.Text;
            estate.EstateType = (EstateType)comboEstateType.SelectedIndex;
            estate.InternalSurfaceArea = txtSurfaceArea.Text;
            estate.Description = txtDescription.Text;
            estate.Address.Street = txtStreet.Text;
            estate.Address.ZipCode = txtZipCode.Text;
            estate.Address.City = txtCity.Text;
            estate.Address.Country = (Countries)comboCountry.SelectedIndex;

            //Attach current displayed image to estate being saved/created
            if (estate != null && pictureEstate.Image != null)
            {
                estate.EstateImage = pictureEstate.Image;
            }

            //Buyer info
            estate.Buyer.Name = txtBuyerName.Text;
            estate.Buyer.Address.Street = txtBuyerStreet.Text;
            estate.Buyer.Address.ZipCode = txtBuyerZipCode.Text;
            estate.Buyer.Address.City = txtBuyerCity.Text;
            estate.Buyer.Address.Country = (Countries)comboBuyerCountry.SelectedIndex;

            //Seller info
            estate.Seller.Name = txtSellerName.Text;
            estate.Seller.Address.Street = txtSellerStreet.Text;
            estate.Seller.Address.ZipCode = txtSellerZipCode.Text;
            estate.Seller.Address.City = txtSellerCity.Text;
            estate.Seller.Address.Country = (Countries)comboSellerCountry.SelectedIndex;

            //Payment info
            SavePayment();

        }

        /// <summary>
        /// Save information from the GUI to create a Rental object through dynamic binding.
        /// </summary>
        private void CreateRental()
        {
            estate = new Rental();

            // Estate stuff
            SaveBaseEstateInfo();

            // Residential stuff
            //TODO move this into the Residential method along with the others
            ((Rental)estate).ResidentialEstateType = ResidentialEstateType.Apartment;
            ((Rental)estate).LegalForm = LegalForm.Rental;
            ((Rental)estate).NumBedrooms = txtResBedrooms.Text;
            ((Rental)estate).NumBathrooms = txtResBathrooms.Text;
            ((Rental)estate).NumLevels = txtResLevels.Text;

            // Apartment stuff
            ((Rental)estate).FloorNum = txtAptFloorNo.Text;
            ((Rental)estate).ElevatorAccess = checkAptElevAccess.Checked;

            // Rental stuff
            ((Rental)estate).MonthlyRent = txtAptPrice.Text;
            ((Rental)estate).HydroIncluded = checkAptHydroIncl.Checked;

            //Debug.WriteLine(((Rental)estate).ToString());
            return;
        }

        /// <summary>
        /// Save information from the GUI to create a Tenement object through dynamic binding.
        /// </summary>
        private void CreateTenement()
        {
            estate = new Tenement();

            // Estate stuff
            SaveBaseEstateInfo();

            // Residential stuff
            ((Tenement)estate).ResidentialEstateType = ResidentialEstateType.Apartment;
            ((Tenement)estate).LegalForm = LegalForm.Tenement;
            ((Tenement)estate).NumBedrooms = txtResBedrooms.Text;
            ((Tenement)estate).NumBathrooms = txtResBathrooms.Text;
            ((Tenement)estate).NumLevels = txtResLevels.Text;

            // Apartment stuff
            ((Tenement)estate).FloorNum = txtAptFloorNo.Text;
            ((Tenement)estate).ElevatorAccess = checkAptElevAccess.Checked;

            // Tenement stuff
            ((Tenement)estate).ListPrice = txtAptPrice.Text;
            return;
        }

        /// <summary>
        /// Save information from the GUI to create a Villa object through dynamic binding.
        /// </summary>
        private void CreateVilla()
        {
            estate = new Villa();

            // Estate stuff
            SaveBaseEstateInfo();

            // Residential stuff
            ((Villa)estate).ResidentialEstateType = ResidentialEstateType.Villa;
            ((Villa)estate).LegalForm = (LegalForm)comboResLegalForm.SelectedIndex;
            ((Villa)estate).NumBedrooms = txtResBedrooms.Text;
            ((Villa)estate).NumBathrooms = txtResBathrooms.Text;
            ((Villa)estate).NumLevels = txtResLevels.Text;

            // Villa Stuff 
            ((Villa)estate).ListPrice = txtVillaPrice.Text;
            ((Villa)estate).NumGarageSpots = txtVillaGrgPrkngSpots.Text;
            ((Villa)estate).GreenSpaceArea = txtVillaYardArea.Text;
            return;
        }

        /// <summary>
        /// Save information from the GUI to create a Rowhouse object through dynamic binding.
        /// </summary>
        private void CreateRowhouse()
        {
            estate = new Rowhouse();

            // Estate stuff
            SaveBaseEstateInfo();

            // Residential stuff
            ((Rowhouse)estate).ResidentialEstateType = ResidentialEstateType.Rowhouse;
            ((Rowhouse)estate).LegalForm = (LegalForm)comboResLegalForm.SelectedIndex;
            ((Rowhouse)estate).NumBedrooms = txtResBedrooms.Text;
            ((Rowhouse)estate).NumBathrooms = txtResBathrooms.Text;
            ((Rowhouse)estate).NumLevels = txtResLevels.Text;

            // Inherited Villa Stuff 
            ((Rowhouse)estate).ListPrice = txtVillaPrice.Text;
            ((Rowhouse)estate).NumGarageSpots = txtVillaGrgPrkngSpots.Text;
            ((Rowhouse)estate).GreenSpaceArea = txtVillaYardArea.Text;

            // Rowhouse stuff
            ((Rowhouse)estate).HasDriveway = checkRowDrivway.Checked;
            ((Rowhouse)estate).HasPrivYard = checkRowPrivYard.Checked;
            return;
        }

        //Commercial 

        /// <summary>
        /// Create a Commercial Estate.
        /// </summary>
        private void CreateCommercial()
        {
            //Determine which Commercial object to create
            CommercialEstateType commEstateType = (CommercialEstateType)comboCommEstateType.SelectedIndex;
            switch (commEstateType)
            {
                case CommercialEstateType.Shop:
                    CreateShop();
                    break;
                case CommercialEstateType.Warehouse:
                    CreateWarehouse();
                    break;
                default:
                    Debug.WriteLine("Error: CommEstateType to create not found.");
                    break;
            }
            return;
        }

        /// <summary>
        /// Save information from the GUI to create a Shop object through dynamic binding.
        /// </summary>
        private void CreateShop()
        {
            estate = new Shop();

            // Estate stuff
            SaveBaseEstateInfo();

            // Commmercial stuff
            ((Shop)estate).CommericalEstateType = CommercialEstateType.Shop;
            ((Shop)estate).Cost = txtCommCost.Text;
            ((Shop)estate).ZoningType = txtCommZoneType.Text;

            // Shop stuff
            ((Shop)estate).IsDetached = checkShopDetached.Checked;
            ((Shop)estate).NumOfFloors = txtShopNumFloors.Text;
            return;
        }

        /// <summary>
        /// Save information from the GUI to create a Warehouse object through dynamic binding.
        /// </summary>
        private void CreateWarehouse()
        {
            estate = new Warehouse();

            // Estate stuff
            SaveBaseEstateInfo();

            // Commmercial stuff
            ((Warehouse)estate).CommericalEstateType = CommercialEstateType.Warehouse;
            ((Warehouse)estate).Cost = txtCommCost.Text;
            ((Warehouse)estate).ZoningType = txtCommZoneType.Text;

            // Warehouse stuff
            ((Warehouse)estate).LotSurfaceArea = txtWareSurfaceArea.Text;
            ((Warehouse)estate).NumLoadingBays = txtWareNumLoadingBays.Text;
            return;
        }

        //Institutional 

        /// <summary>
        /// Create an Institutional Estate.
        /// </summary>
        private void CreateInstitutional()
        {
            //Determine which Institutional object to create
            InstitutionalEstateType instEstateType = (InstitutionalEstateType)comboInstEstateType.SelectedIndex;
            switch (instEstateType)
            {
                case InstitutionalEstateType.School:
                    CreateSchool();
                    break;
                case InstitutionalEstateType.University:
                    CreateUniversity();
                    break;
                case InstitutionalEstateType.Hospital:
                    CreateHospital();
                    break;
                default:
                    Debug.WriteLine("Error: InstEstateType to create not found.");
                    break;
            }
            return;
        }

        /// <summary>
        /// Save information from the GUI to create a School object through dynamic binding.
        /// </summary>
        private void CreateSchool()
        {
            estate = new School();

            // Estate stuff
            SaveBaseEstateInfo();

            // Institutional stuff
            ((School)estate).InstitutionalEstateType = InstitutionalEstateType.School;
            ((School)estate).NumOfBathrooms = txtInstBathrooms.Text;
            ((School)estate).ParkingCapacity = txtInstParkingCap.Text;

            // School stuff
            ((School)estate).NumClassrooms = txtSchoolNumClassrooms.Text;
            ((School)estate).NumFloors = txtSchoolNumFloors.Text;
            return;
        }

        /// <summary>
        /// Save information from the GUI to create a University object through dynamic binding.
        /// </summary>
        private void CreateUniversity()
        {
            estate = new University();

            // Estate stuff
            SaveBaseEstateInfo();

            // Institutional stuff
            ((University)estate).InstitutionalEstateType = InstitutionalEstateType.University;
            ((University)estate).NumOfBathrooms = txtInstBathrooms.Text;
            ((University)estate).ParkingCapacity = txtInstParkingCap.Text;

            // University stuff 
            ((University)estate).NumBuildings = txtUniBuildings.Text;
            ((University)estate).NumLectureHalls = txtUniNumLecHalls.Text;
            ((University)estate).OnCampusResidenceCapacity = txtUniResCap.Text;
            return;
        }

        /// <summary>
        /// Save information from the GUI to create a Hospital object through dynamic binding.
        /// </summary>
        private void CreateHospital()
        {
            estate = new Hospital();

            // Estate stuff
            SaveBaseEstateInfo();

            // Institutional stuff
            ((Hospital)estate).InstitutionalEstateType = InstitutionalEstateType.Hospital;
            ((Hospital)estate).NumOfBathrooms = txtInstBathrooms.Text;
            ((Hospital)estate).ParkingCapacity = txtInstParkingCap.Text;

            // Hospital stuff
            ((Hospital)estate).NumBeds = txtHospNumBeds.Text;
            ((Hospital)estate).SpecialServices = txtHospSpecialServices.Text;
            return;
        }

        /// <summary>
        /// Save any Payment information provided.
        /// </summary>
        private void SavePayment()
        {
            //Determine which Payment object to create
            PaymentType paymentType = (PaymentType)comboPayType.SelectedIndex;
            switch (paymentType)
            {
                case PaymentType.Bank:
                    SaveBank();
                    break;
                case PaymentType.Western_Union:
                    SaveWesternUnion();
                    break;
                case PaymentType.PayPal:
                    SavePayPal();
                    break;
                default:
                    estate.Payment = new Payment(-1, txtPayAmount.Text, txtPayCurrency.Text);
                    Debug.WriteLine("PaymentType to save not found. Saving regular Payment.");
                    break;
            }
            return;
        }

        /// <summary>
        /// Save information from the GUI to create a Bank object through dynamic binding.
        /// </summary>
        private void SaveBank()
        {
            estate.Payment = new Bank();

            //Payment stuff
            SaveBasePaymentInfo();

            ((Bank)estate.Payment).BankName = txtPay1.Text;  //txtPay1 is Bank Name
            ((Bank)estate.Payment).AccountNum = txtPay2.Text;  //txtPay2 is Account Num
            return;
        }

        /// <summary>
        /// Save information from the GUI to create a WesternUnion object through dynamic binding.
        /// </summary>
        private void SaveWesternUnion()
        {
            estate.Payment = new WesternUnion();

            //Payment stuff
            SaveBasePaymentInfo();

            ((WesternUnion)estate.Payment).AccountNum = txtPay1.Text;  //txtPay2 is Account Num
            return;
        }

        /// <summary>
        /// Save information from the GUI to create a PayPal object through dynamic binding.
        /// </summary>
        private void SavePayPal()
        {
            estate.Payment = new PayPal();

            //Payment stuff
            SaveBasePaymentInfo();

            ((PayPal)estate.Payment).Email = txtPay1.Text;  //txtPay1 is Email
            ((PayPal)estate.Payment).CardNum = txtPay2.Text;  //txtPay2 is Card Num
            return;
        }

        /// <summary>
        /// Helper method to save Payment info which doesn't required typecasting.
        /// </summary>
        private void SaveBasePaymentInfo()
        {
            estate.Payment.Amount = txtPayAmount.Text;
            estate.Payment.Currency = txtPayCurrency.Text;
            estate.Payment.PaymentType = comboPayType.SelectedIndex;
            return;
        }

        #endregion

        #region Validation

        /// <summary>
        /// Validate information fields in the GUI 
        /// and provide messages to help them fix mistakes.
        /// </summary>
        /// <returns>Returns true if all fields have valid input.</returns>
        private bool ValidateAllRelevantInputFields()
        {
            bool isValid = true;
            bool paymentIsValid = true;

            //Validate general Estate information first, then start to dig down.
            if (ValidateEstateInfoFields() && ValidateAddressFields())
            {
                EstateType estateType = (EstateType)comboEstateType.SelectedIndex;

                switch (estateType)
                {
                    case EstateType.Residential:
                        isValid = ValidateResInfoFields();
                        break;

                    case EstateType.Commercial:
                        isValid = ValidateCommInfoFields();
                        break;
                    case EstateType.Institutional:
                        isValid = ValidateInstInfoFields();
                        break;
                    default:
                        Debug.WriteLine("InputError: EstateType to validate not found.");
                        break;
                }

                if (!isValid)
                {
                    MessageBox.Show("Please verify that estate-specific information fields are either empty or filled appropriately (eg. estate types selected & numbers only in number fields).", "Error");
                }

                //Validation of Buyer and Seller not done because fields may be empty and that is okay :)

                // Validate payment
                paymentIsValid = ValidatePaymentFields();
            }
            else
            {
                isValid = false;
                MessageBox.Show("Make sure Estate ID (numeric digits only), an Estate Type is selected, and Address fields on the left are filled correctly.", "Error");
            }

            return (isValid && paymentIsValid);
        }

        /// <summary>
        /// Helper to return validity of Estate Info fields.
        /// </summary>
        /// <returns></returns>
        private bool ValidateEstateInfoFields()
        {
            bool isValid = true;
            //ID not allowed to be empty and has to be a number
            if (txtID.Text.Trim().Length < 1 || !StringChecker.IsEmptyOrNumber(txtID.Text))
            {
                isValid = false;
            }
            //Must select an EstateType
            else if (comboEstateType.SelectedIndex == -1)
            {
                isValid = false;
            }
            //Surface Area must be a number
            else if (!StringChecker.IsEmptyOrNumber(txtSurfaceArea.Text))
            {
                isValid = false;
            }
            return isValid;
        }

        /// <summary>
        /// Helper to return validity of Estate Address Info fields.
        /// </summary>
        /// <returns></returns>
        private bool ValidateAddressFields()
        {
            bool isValid = true;

            //Make sure all Address fields are filled
            if (String.IsNullOrWhiteSpace(txtStreet.Text) 
                || String.IsNullOrWhiteSpace(txtZipCode.Text)
                || String.IsNullOrWhiteSpace(txtCity.Text)
                || comboCountry.SelectedIndex == -1)
            {
                isValid = false;
            }
            return isValid;
        }

        /// <summary>
        /// Return the validity of Res Estate Info fields.
        /// </summary>
        /// <returns></returns>
        private bool ValidateResInfoFields()
        {
            bool isValid = true;

            //Make sure EstateType and LegalForm are filled
            if (comboResEstateType.SelectedIndex == -1 
                || comboResLegalForm.SelectedIndex == -1)
            {
                MessageBox.Show("Please select a Residential Estate Type and Legal Form.", "Error");
                return false;
            }
            //Make sure numbers are in fields
            if (!StringChecker.IsEmptyOrNumber(txtResBedrooms.Text)
                || !StringChecker.IsEmptyOrNumber(txtResBathrooms.Text)
                || !StringChecker.IsEmptyOrNumber(txtResLevels.Text))
            {
                MessageBox.Show("Please make sure appropriate fields only have number", "Error");
                return false;
            }

            //Dive one subclass further
            ResidentialEstateType resEstateType = (ResidentialEstateType)comboResEstateType.SelectedIndex;
            switch (resEstateType)
            {
                case ResidentialEstateType.Apartment:
                    isValid = ValidateAptInfoFields();
                    break;
                case ResidentialEstateType.Rowhouse:
                    isValid = ValidateRowhouseInfoFields();
                    break;
                case ResidentialEstateType.Villa:
                    isValid = ValidateVillaInfoFields();
                    break;
                default:
                    Debug.WriteLine("Error: ResEstateType to validate not found.");
                    break;
            }

            return isValid;
        }

        /// <summary>
        /// Return the validity of Apartment Info fields.
        /// </summary>
        /// <returns></returns>
        private bool ValidateAptInfoFields()
        {
            bool isValid = true;

            //Check for numbers in appropriate fields
            if (!StringChecker.IsEmptyOrNumber(txtAptFloorNo.Text))
            {
                isValid = false;
            }
            return isValid;
        }

        /// <summary>
        /// Return the validity of Villa Info fields.
        /// </summary>
        /// <returns></returns>
        private bool ValidateVillaInfoFields()
        {
            bool isValid = true;

            //Check for numbers in appropriate fields
            if (!StringChecker.IsEmptyOrNumber(txtVillaGrgPrkngSpots.Text)
                || !StringChecker.IsEmptyOrNumber(txtVillaYardArea.Text))
            {
                isValid = false;
            }
            return isValid;
        }

        /// <summary>
        /// Return the validity of Rowhouse Info fields.
        /// </summary>
        /// <returns></returns>
        private bool ValidateRowhouseInfoFields()
        {
            bool isValid = true;

            //Check for numbers in appropriate fields
            if (!StringChecker.IsEmptyOrNumber(txtRowGrgPrkngSpots.Text)
                || !StringChecker.IsEmptyOrNumber(txtRowYardArea.Text))
            {
                isValid = false;
            }
            return isValid;
        }

        /// <summary>
        /// Return the validity of Commercial Estate Info fields.
        /// </summary>
        /// <returns></returns>
        private bool ValidateCommInfoFields()
        {
            bool isValid = true;

            //Make sure EstateType and LegalForm are filled
            if (comboCommEstateType.SelectedIndex == -1)
            {
                MessageBox.Show("Please select a Commercial Estate Type.", "Error");
                return false;
            }

            //Dive one subclass further
            CommercialEstateType commEstateType = (CommercialEstateType)comboCommEstateType.SelectedIndex;
            switch (commEstateType)
            {
                case CommercialEstateType.Shop:
                    isValid = ValidateShopInfoFields();
                    break;
                case CommercialEstateType.Warehouse:
                    isValid = ValidateWareInfoFields();
                    break;
                default:
                    Debug.WriteLine("Error: CommEstateType to validate not found.");
                    break;
            }

            return isValid;
        }

        /// <summary>
        /// Return the validity of Shop Info fields.
        /// </summary>
        /// <returns></returns>
        private bool ValidateShopInfoFields()
        {
            bool isValid = true;

            //Check for numbers in appropriate fields
            if (!StringChecker.IsEmptyOrNumber(txtShopNumFloors.Text))
            {
                isValid = false;
            }
            return isValid;
        }

        /// <summary>
        /// Return the validity of Warehouse Info fields.
        /// </summary>
        /// <returns></returns>
        private bool ValidateWareInfoFields()
        {
            bool isValid = true;

            //Check for numbers in appropriate fields
            if (!StringChecker.IsEmptyOrNumber(txtWareSurfaceArea.Text)
                || !StringChecker.IsEmptyOrNumber(txtWareNumLoadingBays.Text))
            {
                isValid = false;
            }
            return isValid;
        }

        /// <summary>
        /// Return the validity of Institutional Estate Info fields.
        /// </summary>
        /// <returns></returns>
        private bool ValidateInstInfoFields()
        {
            bool isValid = true;

            //Make sure EstateType and LegalForm are filled
            if (comboInstEstateType.SelectedIndex == -1)
            {
                MessageBox.Show("Please select an Institutional Estate Type.", "Error");
                return false;
            }
            //Make sure numbers are in appropriate fields
            if (!StringChecker.IsEmptyOrNumber(txtInstParkingCap.Text)
                || !StringChecker.IsEmptyOrNumber(txtInstBathrooms.Text))
            {
                return false;
            }

            //Dive one subclass further
            InstitutionalEstateType instEstateType = (InstitutionalEstateType)comboInstEstateType.SelectedIndex;
            switch (instEstateType)
            {
                case InstitutionalEstateType.University:
                    isValid = ValidateUniInfoFields();
                    break;
                case InstitutionalEstateType.School:
                    isValid = ValidateSchoolInfoFields();
                    break;
                case InstitutionalEstateType.Hospital:
                    isValid = ValidateHospInfoFields();
                    break;
                default:
                    Debug.WriteLine("Error: InstEstateType to validate not found.");
                    break;
            }

            return isValid;
        }

        /// <summary>
        /// Return the validity of Uni Info fields.
        /// </summary>
        /// <returns></returns>
        private bool ValidateUniInfoFields()
        {
            bool isValid = true;

            //Check for numbers in appropriate fields
            if (!StringChecker.IsEmptyOrNumber(txtUniBuildings.Text)
                || !StringChecker.IsEmptyOrNumber(txtUniNumLecHalls.Text)
                || !StringChecker.IsEmptyOrNumber(txtUniResCap.Text))
            {
                isValid = false;
            }
            return isValid;
        }

        /// <summary>
        /// Return the validity of School Info fields.
        /// </summary>
        /// <returns></returns>
        private bool ValidateSchoolInfoFields()
        {
            bool isValid = true;

            //Check for numbers in appropriate fields
            if (!StringChecker.IsEmptyOrNumber(txtSchoolNumClassrooms.Text)
                || !StringChecker.IsEmptyOrNumber(txtSchoolNumFloors.Text))
            {
                isValid = false;
            }
            return isValid;
        }

        /// <summary>
        /// Return the validity of Hospital Info fields.
        /// </summary>
        /// <returns></returns>
        private bool ValidateHospInfoFields()
        {
            bool isValid = true;

            //Check for numbers in appropriate fields
            if (!StringChecker.IsEmptyOrNumber(txtHospNumBeds.Text))
            {
                isValid = false;
            }
            return isValid;
        }

        /// <summary>
        /// Return the validity of Payment Info fields.
        /// </summary>
        /// <returns></returns>
        private bool ValidatePaymentFields()
        {
            bool isValid = true;

            if (!StringChecker.IsEmptyOrNumber(txtPayAmount.Text)
                || !StringChecker.IsEmptyOrLetters(txtPayCurrency.Text))
            {
                MessageBox.Show("Please ensure Payment Amount only contains numbers and Currency only contains characters.", "Error");
                return false;
            }

            //Dive one subclass further
            PaymentType paymentType = (PaymentType)comboPayType.SelectedIndex;
            switch (paymentType)
            {
                case PaymentType.Bank:
                    if (!StringChecker.IsEmptyOrNumber(txtPay2.Text))  //Check that the Account Num is a number
                    {
                        MessageBox.Show("Please ensure Payment Account # only consists of digits 0 to 9.", "Error");
                        isValid = false;
                    }
                    break;
                case PaymentType.Western_Union:
                    if (!StringChecker.IsEmptyOrNumber(txtPay1.Text))  //Check that the Account Num is a number
                    {
                        MessageBox.Show("Please ensure Payment Account # only consists of digits 0 to 9.", "Error");
                        isValid = false;
                    }
                    break;
                case PaymentType.PayPal:
                    if (!StringChecker.IsEmptyOrNumber(txtPay2.Text))  //Check that the Card Num is a number
                    {
                        MessageBox.Show("Please ensure Payment Card # only consists of digits 0 to 9.", "Error");
                        isValid = false;
                    }
                    break;
                default:
                    Debug.WriteLine("No PayEstateType to validate.");
                    break;
            }

            return isValid;
        }


        // Helper validaion Method

        /// <summary>
        /// Only allow control characters, digits, decimal point, plus and minus symbol.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void OnlyAllowNumericCharsOnKeyPress(object sender, KeyPressEventArgs e)
        {
            // Only allow control characters, digits, decimal point, plus and minus symbol.
            if (!char.IsControl(e.KeyChar) 
                && !char.IsDigit(e.KeyChar) 
                && (e.KeyChar != '.') 
                && (e.KeyChar != '+') 
                && (e.KeyChar != '-'))
            {
                e.Handled = true;
            }
        }

        #endregion

        #region Display an Estate

        /// <summary>
        /// Method that is called when an already-existing Estate needs to be displayed.
        /// </summary>
        /// <param name="toDisplay">The Estate that will be displayed.</param>
        private void DisplayEstate(Estate toDisplay)
        {
            if (toDisplay == null)
            {
                Debug.WriteLine("Estate to display is null.");
                return;
            }

            DisplayBaseEstateInfo(toDisplay);

            //Determine which Estate object to display
            switch (toDisplay.EstateType)
            {
                case EstateType.Residential:
                    DisplayResidential((Residential)toDisplay);
                    break;
                case EstateType.Commercial:
                    DisplayCommercial((Commercial)toDisplay);
                    break;
                case EstateType.Institutional:
                    DisplayInstitutional((Institutional)toDisplay);
                    break;
                default:
                    Debug.WriteLine("Error: EstateType to display not found.");
                    break;
            }

            return;
        }

        /// <summary>
        /// Display info of the Estate that doesn't need to be typecast.
        /// </summary>
        /// <param name="toDisplay"></param>
        private void DisplayBaseEstateInfo(Estate toDisplay)
        {
            txtID.Text = toDisplay.ID;
            comboEstateType.SelectedIndex = (int)toDisplay.EstateType;
            txtSurfaceArea.Text = toDisplay.InternalSurfaceArea;
            txtDescription.Text = toDisplay.Description;
            txtStreet.Text = toDisplay.Address.Street;
            txtZipCode.Text = toDisplay.Address.ZipCode;
            txtCity.Text = toDisplay.Address.City;
            comboCountry.SelectedIndex = (int)toDisplay.Address.Country;
            pictureEstate.Image = toDisplay.EstateImage;

            //Buyer info
            txtBuyerName.Text = toDisplay.Buyer.Name;
            txtBuyerStreet.Text = toDisplay.Buyer.Address.Street;
            txtBuyerZipCode.Text = toDisplay.Buyer.Address.ZipCode;
            txtBuyerCity.Text = toDisplay.Buyer.Address.City;
            comboBuyerCountry.SelectedIndex = (int)toDisplay.Buyer.Address.Country;

            //Seller info
            txtSellerName.Text = toDisplay.Seller.Name;
            txtSellerStreet.Text = toDisplay.Seller.Address.Street;
            txtSellerZipCode.Text = toDisplay.Seller.Address.ZipCode;
            txtSellerCity.Text = toDisplay.Seller.Address.City;
            comboSellerCountry.SelectedIndex = (int)toDisplay.Seller.Address.Country;

            //Payment info 
            DisplayPayment(toDisplay);
        }

        //Residential 

        /// <summary>
        /// Display info of the Estate tied to the Residential class.
        /// </summary>
        /// <param name="toDisplay"></param>
        private void DisplayResidential(Residential toDisplay)
        {
            // Residential stuff
            comboResEstateType.SelectedIndex = (int)toDisplay.ResidentialEstateType;
            comboResLegalForm.SelectedIndex = (int)toDisplay.LegalForm;
            txtResBedrooms.Text = toDisplay.NumBedrooms;
            txtResBathrooms.Text = toDisplay.NumBathrooms;
            txtResLevels.Text = toDisplay.NumLevels;

            //Determine which Residential object to display
            ResidentialEstateType resEstateType = toDisplay.ResidentialEstateType;
            switch (resEstateType)
            {
                case ResidentialEstateType.Apartment:
                    DisplayApartment((Apartment)toDisplay);
                    break;
                case ResidentialEstateType.Rowhouse:
                    DisplayRowhouse((Rowhouse)toDisplay);
                    break;
                case ResidentialEstateType.Villa:
                    DisplayVilla((Villa)toDisplay);
                    break;
                default:
                    Debug.WriteLine("Error: ResEstateType to display not found.");
                    break;
            }
            return;
        }

        /// <summary>
        /// Display info of the Estate tied to the Apartment class.
        /// </summary>
        /// <param name="toDisplay"></param>
        private void DisplayApartment(Apartment toDisplay)
        {
            // Apartment stuff
            txtAptFloorNo.Text = toDisplay.FloorNum;
            checkAptElevAccess.Checked = toDisplay.ElevatorAccess;

            //Determine which Apartment object to display
            LegalForm resLegalForm = toDisplay.LegalForm;
            switch (resLegalForm)
            {
                case LegalForm.Rental:
                    DisplayRental((Rental)toDisplay);
                    break;
                case LegalForm.Tenement:
                    DisplayTenement((Tenement)toDisplay);
                    break;
                case LegalForm.Ownership:
                    DisplayTenement((Tenement)toDisplay);
                    break;
                default:
                    Debug.WriteLine("Error: ResEstateType to display not found.");
                    break;
            }
            return;
        }

        /// <summary>
        /// Display info of the Estate tied to the Rental class.
        /// </summary>
        /// <param name="toDisplay"></param>
        private void DisplayRental(Rental toDisplay)
        {
            // Rental stuff
            txtAptPrice.Text = toDisplay.MonthlyRent;
            checkAptHydroIncl.Checked = toDisplay.HydroIncluded;
            return;
        }

        /// <summary>
        /// Display info of the Estate tied to the Tenement class.
        /// </summary>
        /// <param name="toDisplay"></param>
        private void DisplayTenement(Tenement toDisplay)
        {
            // Tenement stuff
            ((Tenement)toDisplay).ListPrice = txtAptPrice.Text;
            txtAptPrice.Text = toDisplay.ListPrice;
            return;
        }

        /// <summary>
        /// Display info of the Estate tied to the Villa class.
        /// </summary>
        /// <param name="toDisplay"></param>
        private void DisplayVilla(Villa toDisplay)
        {
            // Villa Stuff 
            txtVillaPrice.Text = toDisplay.ListPrice;
            txtVillaGrgPrkngSpots.Text = toDisplay.NumGarageSpots;
            txtVillaYardArea.Text = toDisplay.GreenSpaceArea;
            return;
        }

        /// <summary>
        /// Display info of the Estate tied to the Rowhouse class.
        /// </summary>
        /// <param name="toDisplay"></param>
        private void DisplayRowhouse(Rowhouse toDisplay)
        {
            // Inherited Villa Stuff 
            DisplayVilla(toDisplay);

            // Rowhouse stuff
            checkRowDrivway.Checked = toDisplay.HasDriveway;
            checkRowPrivYard.Checked = toDisplay.HasPrivYard;
            return;
        }

        //Commercial 

        /// <summary>
        /// Display info of the Estate tied to the Commercial class.
        /// </summary>
        /// <param name="toDisplay"></param>
        private void DisplayCommercial(Commercial toDisplay)
        {
            // Commmercial stuff
            comboCommEstateType.SelectedIndex = (int)toDisplay.CommericalEstateType;
            txtCommCost.Text = txtCommCost.Text;
            txtCommZoneType.Text = toDisplay.ZoningType;

            //Determine which Commercial object to display
            CommercialEstateType commEstateType = toDisplay.CommericalEstateType;
            switch (commEstateType)
            {
                case CommercialEstateType.Shop:
                    DisplayShop((Shop)toDisplay);
                    break;
                case CommercialEstateType.Warehouse:
                    DisplayWarehouse((Warehouse)toDisplay);
                    break;
                default:
                    Debug.WriteLine("Error: CommEstateType to display not found.");
                    break;
            }
            return;
        }

        /// <summary>
        /// Display info of the Estate tied to the Shop class.
        /// </summary>
        /// <param name="toDisplay"></param>
        private void DisplayShop(Shop toDisplay)
        {
            // Shop stuff
            checkShopDetached.Checked = toDisplay.IsDetached;
            txtShopNumFloors.Text = toDisplay.NumOfFloors;
            return;
        }

        /// <summary>
        /// Display info of the Estate tied to the Warehouse class.
        /// </summary>
        /// <param name="toDisplay"></param>
        private void DisplayWarehouse(Warehouse toDisplay)
        {
            // Warehouse stuff
            txtWareSurfaceArea.Text = toDisplay.LotSurfaceArea;
            txtWareNumLoadingBays.Text = toDisplay.NumLoadingBays;
            return;
        }

        //Institutional 

        /// <summary>
        /// Display info of the Estate tied to the Institutional class.
        /// </summary>
        /// <param name="toDisplay"></param>
        private void DisplayInstitutional(Institutional toDisplay)
        {
            // Institutional stuff
            comboInstEstateType.SelectedIndex = (int)toDisplay.InstitutionalEstateType;
            txtInstBathrooms.Text = toDisplay.NumOfBathrooms;
            txtInstParkingCap.Text = toDisplay.ParkingCapacity;

            //Determine which Institutional object to display
            InstitutionalEstateType instEstateType = toDisplay.InstitutionalEstateType;
            switch (instEstateType)
            {
                case InstitutionalEstateType.School:
                    DisplaySchool((School)toDisplay);
                    break;
                case InstitutionalEstateType.University:
                    DisplayUniversity((University)toDisplay);
                    break;
                case InstitutionalEstateType.Hospital:
                    DisplayHospital((Hospital)toDisplay);
                    break;
                default:
                    Debug.WriteLine("Error: InstEstateType to display not found.");
                    break;
            }
            return;
        }

        /// <summary>
        /// Display info of the Estate tied to the School class.
        /// </summary>
        /// <param name="toDisplay"></param>
        private void DisplaySchool(School toDisplay)
        {
            // School stuff
            txtSchoolNumClassrooms.Text = toDisplay.NumClassrooms;
            txtSchoolNumFloors.Text = toDisplay.NumFloors;
            return;
        }

        /// <summary>
        /// Display info of the Estate tied to the University class.
        /// </summary>
        /// <param name="toDisplay"></param>
        private void DisplayUniversity(University toDisplay)
        {
            // University stuff 
            txtUniBuildings.Text = toDisplay.NumBuildings;
            txtUniNumLecHalls.Text = toDisplay.NumLectureHalls;
            txtUniResCap.Text = toDisplay.OnCampusResidenceCapacity;
            return;
        }

        /// <summary>
        /// Display info of the Estate tied to the Hospital class.
        /// </summary>
        /// <param name="toDisplay"></param>
        private void DisplayHospital(Hospital toDisplay)
        {
            // Hospital stuff
            txtHospNumBeds.Text = toDisplay.NumBeds;
            txtHospSpecialServices.Text = toDisplay.SpecialServices;
            return;
        }

        /// <summary>
        /// Display info of the Estate tied to the Payment class.
        /// </summary>
        /// <param name="toDisplay"></param>
        private void DisplayPayment(Estate toDisplay)
        {
            if (toDisplay == null)
            {
                Debug.WriteLine("Estate to display is null.");
                return;
            }

            //Base estate info
            txtPayAmount.Text = toDisplay.Payment.Amount;
            txtPayCurrency.Text = toDisplay.Payment.Currency;
            comboPayType.SelectedIndex = toDisplay.Payment.PaymentType;

            //Determine which Estate object to display
            switch (toDisplay.Payment.PaymentType)
            {
                case (int)PaymentType.Bank:
                    DisplayBank((Bank)toDisplay.Payment);
                    break;
                case (int)PaymentType.Western_Union:
                    DisplayWesternUnion((WesternUnion)toDisplay.Payment);
                    break;
                case (int)PaymentType.PayPal:
                    DisplayPayPal((PayPal)toDisplay.Payment);
                    break;
                default:
                    Debug.WriteLine("No PaymentType to display.");
                    break;
            }

            return;
        }

        /// <summary>
        /// Display info of the Estate tied to the Bank class.
        /// </summary>
        /// <param name="payment"></param>
        private void DisplayBank(Bank payment)
        {
            //Bank info
            txtPay1.Text = payment.BankName;
            txtPay2.Text = payment.AccountNum;
        }

        /// <summary>
        /// Display info of the Estate tied to the WesternUnion class.
        /// </summary>
        /// <param name="payment"></param>
        private void DisplayWesternUnion(WesternUnion payment)
        {
            //Western Union info
            txtPay1.Text = payment.AccountNum;
        }

        /// <summary>
        /// Display info of the Estate tied to the PayPal class.
        /// </summary>
        /// <param name="payment"></param>
        private void DisplayPayPal(PayPal payment)
        {
            //PayPal info
            txtPay1.Text = payment.Email;
            txtPay2.Text = payment.CardNum;
        }

        #endregion

        #region File Menu Methods

        private void mnuFileNew_Click(object sender, EventArgs e)
        {
            //Ask for confirmation
            var confirmResult = MessageBox.Show("Are you sure you wish to create a new Real Estate collection? Any unsaved data will be lost. This cannot be undone.",
                                     "Confirm New",
                                     MessageBoxButtons.YesNo);
            if (confirmResult == DialogResult.Yes)
            {
                //Clear EstateManger list
                estateManager.CreateNewList();
                
                //Update current filepath
                liveFilepath = "";

                //Clear all fields
                UpdateGUI();
            }
            else
            {
                // If 'No', do nothing.
            }
            return;
        }


        private void mnuFileSaveAs_Click(object sender, EventArgs e)
        {
            if(saveEstateCollectionDialog.ShowDialog() == DialogResult.OK)
            {
                estateManager.BinarySerialize(saveEstateCollectionDialog.FileName);
                //Update current filepath
                liveFilepath = saveEstateCollectionDialog.FileName;
                MessageBox.Show("Estates saved!", "Success");
            }
        }

        private void mnuFileOpen_Click(object sender, EventArgs e)
        {
            if(openEstateCollectionDialog.ShowDialog() == DialogResult.OK)
            {
                estateManager.BinaryDeSerialize(openEstateCollectionDialog.FileName);
                //Update current filepath
                liveFilepath = openEstateCollectionDialog.FileName;
            }
            //Update list with loaded file
            UpdateGUI();
            MessageBox.Show("Estates loaded.", "Success");
        }

        private void mnuFileSave_Click(object sender, EventArgs e)
        {
            if(liveFilepath == "")
            {
                mnuFileSaveAs_Click(sender, e);
            }
            else
            {
                estateManager.BinarySerialize(liveFilepath);
                MessageBox.Show("Estates saved!", "Success");
            }
        }

        #endregion

        private void mnuFileExit_Click(object sender, EventArgs e)
        {
            //Ask for confirmation
            var confirmResult = MessageBox.Show("Are you sure you to close Modern Real Estate? Any unsaved data will be lost.",
                                     "Confirm New",
                                     MessageBoxButtons.YesNo);
            if (confirmResult == DialogResult.Yes)
            {
                Close();
            }
            else
            {
                // If 'No', do nothing.
            }
        }
    }
}
