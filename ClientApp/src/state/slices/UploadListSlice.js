import { createSlice } from '@reduxjs/toolkit';

export const uploadListSlice = createSlice({
    name: "uploadList",
    initialState: [],
    reducers: {
        addItem: (state, action) => {
            state.push(action.payload);
        },
        deleteItem: (state, action) => {
            return state.filter(item => item.id !== action.payload);
        },
        deleteAll: (state) => {
            return [];
        },
        updateItem: (state, action) => {
            let index = state.findIndex(item => item.id === action.payload.id);
            if (index > -1) {
                state[index] = action.payload;
            }
        },
    }
});

export const { addItem, deleteItem, deleteAll, updateItem } = uploadListSlice.actions;

export const uploadListSelector = store => store.uploadList;