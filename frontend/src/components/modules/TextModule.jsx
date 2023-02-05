import { createEffect, createSignal } from "solid-js";

export default function TextModule(props) {
  const [number, setNumber] = createSignal("");

  createEffect( () => {
    props.setFunctions( (functions) => {
        functions[props.index()].options = {
            number: number()
        }
        return functions;
    })
  });

  return (
    <div class="bg-slate-50 p-3 m-3 flex flex-col">
      <div class="flex">
        <h2 class="text-xl mb-2">Text</h2>
      </div>
      <div class="flex rounded border-2 border-slate-200">
        <div class="p-2 px-3">
          <span>Number</span>
        </div>
        <input onInput={(e) => setNumber(e.target.value)} type="text" class="block p-2 w-full" />
      </div>
    </div>
  );
}
