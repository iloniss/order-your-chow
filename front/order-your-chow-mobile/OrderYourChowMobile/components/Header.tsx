import {View, Text} from 'react-native';
import React from 'react';

interface HeaderProps {
  title: string;
}

const Header: React.FC<HeaderProps> = ({title}) => {
  return (
    <View>
      <Text className="font-bold text-xl bg-white py-3 border-gray-400 border-b-2 border-dotted border-spacing-2 text-center">
        {title}
      </Text>
    </View>
  );
};

export default Header;
