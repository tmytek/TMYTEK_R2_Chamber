import clr
import os
import sys
import time 

# 設定DLL路徑
dll_dir = os.path.abspath('.')  # 或指定你的dll資料夾
dll_path = os.path.join(dll_dir, 'Mitsubishi_FX5U.dll')
sys.path.append(dll_dir)
clr.AddReference('Mitsubishi_FX5U')

# 匯入namespace
#from Equipment import Mitsubishi_FX5U
import Mitsubishi_FX5U

# 建立控制物件
fx5u = Mitsubishi_FX5U('COM10')  # 修改為你的實際COM port

# 設定角度
theta_angle = 10 #大轉盤
phi_angle = 20 #DUT轉盤

# 設定馬達角度
fx5u.SetThetaAngle(theta_angle)
fx5u.SetPhiAngle(phi_angle)
time.sleep(5)  # 等待馬達移動到指定角度
# 讀取目前角度
theta_pos = fx5u.ReadThetaPosition()
phi_pos = fx5u.ReadPhiPosition()
print(f"Theta Position: {theta_pos}")
print(f"Phi Position: {phi_pos}")
