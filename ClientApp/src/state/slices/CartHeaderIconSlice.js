import { createSlice } from "@reduxjs/toolkit";

export const cartHeaderIconStates = {
    IDLE: "idle",
    EXCITED: "excited",
};

export const cartHeaderIconSlice = createSlice({
    name: "cartHeaderIcon",
    initialState: {
        animationClasses: "",
    },
    reducers: {
        animate: (state) => {
            state.animationClasses="iconAnimation";
        },
        doneAnimating: (state) => {
            state.animationClasses="";
        }
    }
});

export const { animate, doneAnimating } = cartHeaderIconSlice.actions;

export const cartHeaderIconAnimationClassesSelector = store => store.cartHeaderIcon.animationClasses;