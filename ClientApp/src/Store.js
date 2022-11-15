import { configureStore } from '@reduxjs/toolkit';
import updateListReducer from './components/Upload/UploadList/UploadListSlice';

export default configureStore({
    reducer: {
        uploadList: updateListReducer
    },
});