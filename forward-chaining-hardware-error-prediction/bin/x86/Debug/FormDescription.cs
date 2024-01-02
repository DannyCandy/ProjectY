using DevComponents.WinForms.Drawing;
using NUnit.Framework;
using SbsSW.SwiPlCs;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace hardware_prediction_expert_system
{
    public partial class FormDescription : Form
    {
        private Dictionary<String, String> description = new Dictionary<string, string>();
        ExpertSystem expertSystem = new ExpertSystem();

        public FormDescription()
        {
            InitializeComponent();
            buildBaseRule();
            buildListDescription();
        }

        private void buildListDescription()
        {
            description.Add("monitor_signal_cable", "Lỗi hỏng hóc do màn hình.");
            description.Add("monitor_control_button", "Lỗi hỏng hóc do màn hình.");
            description.Add("monitor", "Lỗi hỏng hóc do màn hình.");

            description.Add("power_cable", "Lỗi hỏng hóc do nguồn");
            description.Add("power", "Lỗi hỏng hóc do nguồn");

            description.Add("cmos", "Lỗi hỏng hóc từ các thiết bị xử lý");
            description.Add("bios", "Lỗi hỏng hóc từ các thiết bị xử lý");
            description.Add("mainboard_clock_chip", "Lỗi hỏng hóc từ các thiết bị xử lý");
            description.Add("mainboard", "Lỗi hỏng hóc từ các thiết bị xử lý");
            description.Add("ram_slot", "Lỗi hỏng hóc từ các thiết bị xử lý");
            description.Add("ram", "Lỗi hỏng hóc từ các thiết bị xử lý");
            description.Add("keyboard_test_chip", "Lỗi hỏng hóc từ các thiết bị xử lý");
            description.Add("vga_card", "Lỗi hỏng hóc từ các thiết bị xử lý");
            description.Add("vga_card_slot", "Lỗi hỏng hóc từ các thiết bị xử lý");
            description.Add("card_on_mainboard", "Lỗi hỏng hóc từ các thiết bị xử lý");
            description.Add("cmos_battery", "Lỗi hỏng hóc từ các thiết bị  xử lý");
            description.Add("serial_port", "Lỗi hỏng hóc từ các thiết bị xử lý");
            description.Add("parallel_port", "Lỗi hỏng hóc từ các thiết bị xử lý");
            description.Add("cpu", "Lỗi hỏng hóc từ các thiết bị xử lý");

            description.Add("os", "Lỗi hỏng hóc từ dữ liệu");
            description.Add("hdd_cable", "Lỗi hỏng hóc từ dữ liệu");
            description.Add("hdd", "Lỗi hỏng hóc từ dữ liệu");
        }

        private void buildBaseRule()
        {
            // Add rules
            expertSystem.AddRule("group(power_problem)", new List<string> { "not(on(power_led))", "not(on(monitor))", "not(run(power_fan))" });
            expertSystem.AddRule("group(monitor_problem)", new List<string> { "on(power_led)", "not(on(monitor))", "bip(one)" });
            expertSystem.AddRule("group(processor_problem)", new List<string> { "on(power_led)", "not(on(monitor))" });
            expertSystem.AddRule("group(os_problem)", new List<string> { "on(monitor)", "not(load(os))", "bip(one)" });

            expertSystem.AddRule("reason(power)", new List<string> { "group(power_problem)", "tight(power_cable)" });
            expertSystem.AddRule("reason(power_cable)", new List<string> { "group(power_problem)", "not(tight(power_cable))" });

            expertSystem.AddRule("reason(monitor_signal_cable)", new List<string> { "group(monitor_problem)", "not(tight(monitor_signal_cable))" });
            expertSystem.AddRule("reason(monitor_control_button)", new List<string> { "group(monitor_problem)", "tight(monitor_signal_cable)" });
            expertSystem.AddRule("reason(monitor)", new List<string> { "group(monitor_problem)", "tight(monitor_signal_cable)", "right(monitor_control_button)" });

            expertSystem.AddRule("reason(cmos)", new List<string> { "group(processor_problem)", "bip(one_one_three)" });
            expertSystem.AddRule("reason(bios)", new List<string> { "group(processor_problem)", "bip(one_one_four)" });
            expertSystem.AddRule("reason(mainboard_clock_chip)", new List<string> { "group(processor_problem)", "bip(one_two_one)" });
            expertSystem.AddRule("reason(mainboard)", new List<string> { "group(processor_problem)", "bip(one_three_one)" });
            expertSystem.AddRule("reason(ram_slot)", new List<string> { "group(processor_problem)", "bip(one_four_two)" });
            expertSystem.AddRule("reason(ram)", new List<string> { "group(processor_problem)", "bip(two_zero_zero)" });
            expertSystem.AddRule("reason(keyboard_test_chip)", new List<string> { "group(processor_problem)", "bip(three_two_four)" });
            expertSystem.AddRule("reason(vga_card_slot)", new List<string> { "group(processor_problem)", "bip(three_three_four)" });
            expertSystem.AddRule("reason(vga_card)", new List<string> { "group(processor_problem)", "bip(three_four_zero)" });
            expertSystem.AddRule("reason(card_on_mainboard)", new List<string> { "group(processor_problem)", "bip(four_two_four)" });
            expertSystem.AddRule("reason(cmos_battery)", new List<string> { "group(processor_problem)", "bip(four_three_four)" });
            expertSystem.AddRule("reason(serial_port)", new List<string> { "group(processor_problem)", "bip(four_four_one)" });
            expertSystem.AddRule("reason(parallel_port)", new List<string> { "group(processor_problem)", "bip(four_four_two)" });
            expertSystem.AddRule("reason(cpu)", new List<string> { "group(processor_problem)", "bip(four_four_three)" });

            expertSystem.AddRule("reason(hdd)", new List<string> { "group(os_problem)", "bad_msg(hdd)", "tight(hdd_cab)" });
            expertSystem.AddRule("reason(hdd_cable)", new List<string> { "group(os_problem)", "bad_msg(hdd)", "not(tight(hdd_cab))" });
            expertSystem.AddRule("reason(os)", new List<string> { "group(os_problem)", "not(bad_msg(hdd))" });
            
        }

        private void FormDescription_Load(object sender, EventArgs e)
        {
            reset_btn.Focus();
            reset();
        }

        private void reset()
        {
            power_led_cbb.SelectedIndex = 0;
            power_fan_cbb.SelectedIndex = 0;
            monitor_cbb.SelectedIndex = 0;
            bip_des_cbb.SelectedIndex = 0;
            os_cbb.SelectedIndex = 0;
            power_cable_cbb.SelectedIndex = 0;
            monitor_signal_cable_cbb.SelectedIndex = 0;
            monitor_control_button_cbb.SelectedIndex = 0;
            hdd_cab_cbb.SelectedIndex = 0;
            hdd_cbb.SelectedIndex = 0;
        }

        private void reset_btn_Click(object sender, EventArgs e)
        {
            
            collectResult();
            List<string> queryResult = expertSystem.RunInference();
            string str = string.Join(" ", queryResult);
            MessageBox.Show(str, "Kết quả chẩn đoán!!!", MessageBoxButtons.OK, MessageBoxIcon.Information);

        }

        private void collectResult()
        {
            expertSystem.ClearFacts();
            if (power_led_cbb.SelectedIndex != 0)
            {
                expertSystem.AddFact("on(power_led)");
            }
            else
            {
                expertSystem.AddFact("not(on(power_led))");
            }

            if (monitor_cbb.SelectedIndex != 0)
            {
                expertSystem.AddFact("on(monitor)");
            }
            else
            {
                expertSystem.AddFact("not(on(monitor))");
            }

            if (power_fan_cbb.SelectedIndex != 0)
            {
                expertSystem.AddFact("run(power_fan)");
            }
            else
            {
                expertSystem.AddFact("not(run(power_fan))");
            }

            if (os_cbb.SelectedIndex != 0)
            {
                expertSystem.AddFact("load(os)");
            }
            else
            {
                expertSystem.AddFact("not(load(os))");
            }

            if (power_cable_cbb.SelectedIndex != 0)
            {
                expertSystem.AddFact("tight(power_cable)");
            }
            else
            {
                expertSystem.AddFact("not(tight(power_cable))");
            }

            if (monitor_signal_cable_cbb.SelectedIndex != 0)
            {
                expertSystem.AddFact("tight(monitor_signal_cable)");
            }
            else
            {
                expertSystem.AddFact("not(tight(monitor_signal_cable))");
            }

            if (monitor_control_button_cbb.SelectedIndex != 0)
            {
                expertSystem.AddFact("right(monitor_control_button)");
            }
            else
            {
                expertSystem.AddFact("not(right(monitor_control_button))");
            }

            if (hdd_cab_cbb.SelectedIndex != 0)
            {
                expertSystem.AddFact("tight(hdd_cab)");
            }
            else
            {
                expertSystem.AddFact("not(tight(hdd_cab))");
            }

            if (hdd_cbb.SelectedIndex != 0)
            {
                expertSystem.AddFact("bad_msg(hdd)");
            }
            else
            {
                expertSystem.AddFact("not(bad_msg(hdd))");
            }


            if (bip_des_cbb.SelectedIndex != 0)
            {
                expertSystem.AddFact("bip("+bip_des_cbb.SelectedItem.ToString()+")");
            }
        }

        private void finish_btn_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show("Bạn có chắc chắn muốn xem kết quả chẩn đoán?", "Xác nhận", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (result == DialogResult.Yes)
            {
                try
                {
                    collectResult();
                    List<string> queryResult = expertSystem.RunInference();
                    string reason = queryResult.LastOrDefault(item => item.StartsWith("reason(") && item.EndsWith(")"));
                    if (reason != null)
                    {
                        Regex regex = new Regex(@"\((.*?)\)");
                        Match match = regex.Match(reason);
                        if (match.Success)
                        {
                            string errorDevice = match.Groups[1].Value;
                            MessageBox.Show(description[errorDevice]+": "+errorDevice, "Kết quả chẩn đoán!!!", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }
                    }
                    else
                    {
                        MessageBox.Show("Lỗi không có sẵn trong cơ sở tri thức.\r\nChúng tôi sẽ nghiên cứu và bổ sung nhiều chẩn đoán lỗi hơn trong thời gian tới!", "Opps ...", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Lỗi hệ thống!\r\n"+ex, "Opps ...", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
        }

    }
}
