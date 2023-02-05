import {firebase} from '@react-native-firebase/storage';

export const getImgUri = async (imageName: string): Promise<string> => {
  const ref = firebase.storage().ref(imageName);
  return await ref.getDownloadURL();
};
