import { createEffect, createSignal } from "solid-js";

export default function FetchModule(props) {
  const [url, setUrl] = createSignal("");

  createEffect( () => {
    props.setFunctions( (functions) => {
        functions[props.index()].options = {
            url: url()
        }
        return functions;
    })
  });

  return (
    <div class="p-3 m-3 flex flex-col outline outline-gray-400 rounded">
      <div class="flex">
        <h2 class="text-xl font-bold mb-2">Fetch</h2>
      </div>
      <div class="flex rounded border-2 border-slate-300">
        <div class="p-2 px-3">
          <span className="text-gray-700 ">URL</span>
        </div>
        <input onInput={(e) => setUrl(e.target.value)} type="text" class="block p-2 w-full rounded text-gray-700 font-bold" />
      </div>
    </div>
  );
}
