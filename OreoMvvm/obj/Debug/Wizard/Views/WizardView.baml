   M S B A M L   `   `   `  ����C  ?OreoMvvm, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null1clr-namespace:OreoMvvm.WizardOreoMvvm.Wizard  5      =#clr-namespace:OreoMvvm.Wizard.ViewsOreoMvvm.Wizard.Views  5      G(clr-namespace:OreoMvvm.Wizard.ViewModelsOreoMvvm.Wizard.ViewModels  5	      &     OreoMvvm.Wizard.Views.WizardView   5      \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5      R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5      @d2http://schemas.microsoft.com/expression/blend/2008     5      /localclr-namespace:OreoMvvm.Wizard     5      4view#clr-namespace:OreoMvvm.Wizard.Views     5      >	viewModel(clr-namespace:OreoMvvm.Wizard.ViewModels     5	        � 	Resources  5
       �� 
TargetType �� Property �� Value �� 
TargetType J� RenderTransformOrigin J� Opacity �� RadiusX �� RadiusY	 J� Effect3  -System.Windows.Media.Effects.DropShadowEffect
   Opacity �� Angle   wizardStepTemplate ��Name �� CornerRadius J� SnapsToDevicePixels �� Padding ��Name �� Path �� Triggers �� Value �� Binding �� 
TargetName��%�  '��       &	  �    �� 5   
   " ��6   .�5      ��  � �� 5      �� 5      " ��6&   .R�5      �� $	��2,0q�6%   .+�5      � $��1,1,1,12q�6#   $ 0.5,0.5&�5   #   .+�5   "   �� 
�������$��3��5   ,   $
��10,0q�5   ,   $	 0.6Q�5   ,   $ 8��5   ,   $ 8��5   ,   $��Bottom=�5   ,   �� 5   "   $��Header��63   $��22(�5   3   $��DemiBold"�5   3   
���P��$��Center=�5   3   $��Center=�5   3   	 5    "     5!   &   $	
 0.1Q�67   z�5#   "   �� 5$   &   $ 0Q�66   5&      � 5'      .+�5(   "   �� #��z��6,   �� 5)   "   $��Content��63   5*      5+      5,      5.      5/      �� 52   
   .R�53      �� $ bdOuter��6   
���   �$��0,0,1,1q�55      $ 0��56      $��1,1,1,12q�57      $	 0.5Q�58       . ._�5:      �� $ bdInner��6   ���	#FFFEFEFE
�������$��2,2,1,1q�5=      $ 0��5>      $ 2q�5?      ._�5A      � .+�5B      �� $	 txt��6$   $��4,0,0,0q�5C   $   ��5D   $   �� $ ViewModel.DisplayName�5E      5F      5G       5I      �� 5J      $
 True��5K       5J      �� $ ViewModel.IsCurrentStep�.a�5L      �� $	 txt��6    � ��$
 Bold"�5N      �� 5O      $ bdInner��6    � ��
 �����5Q      �� 5R      $ bdOuter��6    �@J�Opacity$ 1Q�5T      5U      �� 5W      $ False��5X       5W      �� $ Relevant�.a�5Y      �� $ bdOuter��6    � y�$ 	Collapsed=�5[      5\      �� 5]      $ False��5^       5]      �� $ Visited�.a�5_      �� $ bdOuter��6    � y�$ 	Collapsed=�5a      5b      5d      5e      .��5g      � 
�������$��1q�5h       � ColumnDefinitions 5i   
   �� 5j      $	��200 �6   �� 5k      $��* �6    � RowDefinitions 5m   
   �� 5n      $��* �6   �� 5o      $��30 �6   .+�5s   
   �� $��Steps��6!   .��5t      �� #��[  5u      ��5t      �� $ Steps�5u      5v      �� 5y   
   
�������$��1��5z      $��0��5{      ._�5}      �� ��6%   �� $+ %CurrentLinkedListStep.Value.ViewModel���5~   %   �� $7 1CurrentLinkedListStep.Value.ViewModel.DisplayName�  5      +  %OreoMvvm.Wizard.StepTemplateConverter  5�        KeystepTemplateConverter 62   ��5�      �� 5�       �� BasedOn# [B��6   " ��5�      .�5�      ��  � �� 5�   !   �� $- 'DataContext.CurrentLinkedListStep.Value� �� RelativeSource ��  �� Mode$ FindAncestor=� �� AncestorType" >�  stepTemplateConverter �� 	Converter# [ 5�      5�      5�      5�      5�      