   M S B A M L   `   `   `  ����  OreoMvvm1    +OreoMvvm.Wizard.Views.SingleOptionGroupView   5      \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5      R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5      @d2http://schemas.microsoft.com/expression/blend/2008     5        � 	Resources  5       �� 
TargetType   messageStyle �� Property �� Value��%�   &	        �� 5	   
   " ��6   .�5      ��  � ��$ 12,0,0,0q�5      �� 5       � ��$	 Top=�5      5      5      .��5      � .+�5   
   �� -   5         ��Name$	 grp��5      $��2q�5      ��5      ��  �� Path$ DisplayName�.��5      �� ��6   �� $ OptionModels���5      �� 5      .R�5      ��  �� Orientation$ 
Horizontal=�6%   .+�5      	� $��2,0,0,0q�5   *    	� 	GroupName 5   *   ��  �� RelativeSource �� 	 �� AncestorType$	 GroupBoxL�$ DataContext.DisplayName�{�5   *   �� $ Enabled�
 d� 	IsChecked
 5   *   �� $ 
IsSelected��� 5       $��Center=�5!   $    �� Target 5    $   ��  �� $	 
StackPanelL�$ Children[0]�.��5#   "   �� ��6-   �� $ DisplayName� �� StringFormat$
 _{0}��5$      �� 5%      #��[  6)   .+�5&   "   �� ��60   ��  �� $	 
StackPanelL�$&  Children[0].DataContext.Messages���5'   &   �� 5(   *   .R�5)   .   �� 
���r�����65   �� 5*   K   5+   +   5-   #   5.      5/      50      52      53      54      55      